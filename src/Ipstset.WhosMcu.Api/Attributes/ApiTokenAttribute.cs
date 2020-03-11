using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Attributes
{
    public class ApiTokenAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //get header
            var apiSession = context.HttpContext.Request.Headers[Constants.ApiTokenHeader];
            if (string.IsNullOrEmpty(apiSession))
            {
                context.Result = new ObjectResult(new
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Message = $"{Constants.ApiTokenHeader} missing from request header"
                })
                { StatusCode = (int)HttpStatusCode.BadRequest };
                return;
            }

            base.OnActionExecuting(context);
        }
        
    }
}
