using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ipstset.WhosMcu.Api
{
    public class BaseController : Controller
    {
        //todo: use attribute
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //add session cookie if not present
            var cookie = context.HttpContext.Request.Cookies[Constants.SessionCookie];
            if (cookie == null)
            {
                var options = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTime.Now.AddYears(1)
                };

                context.HttpContext.Response.Cookies.Append(Constants.SessionCookie, Guid.NewGuid().ToString(), options);
            }

            base.OnActionExecuting(context);
        }
    }
}
