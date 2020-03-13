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
            if(context.HttpContext.Items.ContainsKey(Constants.HttpContextItems.IgnoreApiTokenKey))
            {
                bool.TryParse(context.HttpContext.Items[Constants.HttpContextItems.IgnoreApiTokenKey].ToString(), out bool ignoreApiToken);
                if (ignoreApiToken)
                    return;
            }

            //todo: log if unauthorized?
            var token = context.HttpContext.Request.Headers[Constants.ApiTokenHeader];
            if (string.IsNullOrEmpty(token) || !_apiTokenManager.ValidateToken(token))
            {
                context.Result = new ObjectResult(null) { StatusCode = 401 };
            }
            else
            {
                var sessionId = _apiTokenManager.ReadToken(token).Subject;
                context.HttpContext.Items[Constants.HttpContextItems.WmSessionId] = sessionId;
            }

        }

    }


}
