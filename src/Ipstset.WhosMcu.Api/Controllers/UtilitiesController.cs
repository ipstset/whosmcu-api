using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Api.Logging;
using Ipstset.WhosMcu.Api.Attributes;
using Ipstset.WhosMcu.Api.ApiTokens;

namespace Ipstset.WhosMcu.Api.Controllers
{
    [Route("utilities")]
    [ApiController]
    public class UtilitiesController : BaseController
    {
        private IApiTokenManager _apiTokenManager;

        public UtilitiesController(IApiTokenManager apiTokenManager)
        {
            _apiTokenManager = apiTokenManager;
        }

        [Route("requestlog")]
        [HttpGet]
        public ActionResult<RequestLog> RequestLog()
        {
            var token = Request.Headers[Constants.ApiTokenHeader];
            if (!string.IsNullOrEmpty(token) && _apiTokenManager.ValidateToken(token))
            {
                var sessionId = _apiTokenManager.ReadToken(token).Subject;
                HttpContext.Items[Constants.HttpContextItems.WmSessionId] = sessionId;
            }

            var log = Ipstset.WhosMcu.Api.Logging.RequestLog.Create(RouteData, HttpContext);
            return log;
        }
    }
}
