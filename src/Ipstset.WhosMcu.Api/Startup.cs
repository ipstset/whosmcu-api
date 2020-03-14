using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Api.ApiTokens;
using Ipstset.WhosMcu.Api.Attributes;
using Ipstset.WhosMcu.Api.Logging;
using Ipstset.WhosMcu.Application.Actors;
using Ipstset.WhosMcu.Application.Behaviors;
using Ipstset.WhosMcu.Application.McuActors;
using Ipstset.WhosMcu.Application.McuActors.SearchMcuActors;
using Ipstset.WhosMcu.Application.Movies;
using Ipstset.WhosMcu.Infrastructure.SqlData;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ipstset.WhosMcu.Api
{
    public class Startup
    {
        private string _contentRoot;
        private string _whosMcuConnection;
        private ApiTokenSettings _apiTokenSettings;
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            _contentRoot = env.ContentRootPath;

            _whosMcuConnection = env.EnvironmentName == "DevelopmentLocal" ? 
                Configuration["ConnectionStrings:WhosMcu"] : 
                Environment.GetEnvironmentVariable("WHOSMCU_CONNECTION");

            _apiTokenSettings = new ApiTokenSettings
            {
                Issuers = Configuration["ApiTokenSettings:Issuers"].Split(","),
                Audiences = Configuration["ApiTokenSettings:Audiences"].Split(","),
                MinutesToExpire = Convert.ToInt32(Configuration["ApiTokenSettings:MinutesToExpire"]),
                Secret = env.EnvironmentName == "DevelopmentLocal" ?
                        Configuration["ApiTokenSettings:Secret"] :
                        Environment.GetEnvironmentVariable("API_TOKEN_SECRET")
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(ApiTokenServiceFilter));
                options.Filters.Add(typeof(LogRequestServiceFilter));
            });

            #region Repository injection

            var db = new DbSettings
            {
                Connection = _whosMcuConnection,
                Schema = "dbo",
            };

            services.AddTransient<IMcuActorReadOnlyRepository, McuActorReadOnlyRepository>((ctx) => new McuActorReadOnlyRepository(db));
            services.AddTransient<IMovieReadOnlyRepository, MovieReadOnlyRepository>((ctx) => new MovieReadOnlyRepository(db));
            services.AddTransient<IActorReadOnlyRepository, ActorReadOnlyRepository>((ctx) => new ActorReadOnlyRepository(db));

            services.AddTransient<ILogRepository, LogRepository>();
            #endregion

            services.AddTransient<IApiTokenManager, ApiTokenManager>((ctx) => new ApiTokenManager(_apiTokenSettings));
            services.AddScoped<ApiTokenServiceFilter>();
            services.AddScoped<LogRequestServiceFilter>();

            #region Mediatr
            services.AddMediatR(typeof(SearchMcuActorsHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            #endregion

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());

                options.DefaultPolicyName = "CorsPolicy";
            });

            //RESPONSE JSON FORMATTING
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //needed for Heroku
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
