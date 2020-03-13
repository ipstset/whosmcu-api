using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Attributes
{
    public class IgnoreApiTokenAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items[Constants.HttpContextItems.IgnoreApiTokenKey] = true;
            base.OnActionExecuting(context);
        }
    }
}
