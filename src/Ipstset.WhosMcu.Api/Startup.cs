using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Application.Behaviors;
using Ipstset.WhosMcu.Application.McuActors;
using Ipstset.WhosMcu.Application.McuActors.SearchMcuActors;
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
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            _contentRoot = env.ContentRootPath;
            _whosMcuConnection = Environment.GetEnvironmentVariable("WHOSMCU_CONNECTION");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Repository injection

            var db = new DbSettings
            {
                Connection = _whosMcuConnection,
                Schema = Configuration["DbSettings:Schema"],
            };

            services.AddTransient<IMcuActorReadOnlyRepository, McuActorReadOnlyRepository>((ctx) => new McuActorReadOnlyRepository(db));
            #endregion

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
