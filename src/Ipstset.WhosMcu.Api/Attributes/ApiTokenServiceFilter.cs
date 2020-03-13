using Ipstset.WhosMcu.Api.ApiTokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Attributes
{
    public class ApiTokenServiceFilter : IActionFilter
    {
        private IApiTokenManager _apiTokenManager;
        public ApiTokenServiceFilter(IApiTokenManager apiTokenManager)
        {
            _apiTokenManager = apiTokenManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers[Constants.ApiTokenHeader];
            if (!string.IsNullOrEmpty(token))
                _apiTokenManager.ValidateToken(token);
        }

    }


}
