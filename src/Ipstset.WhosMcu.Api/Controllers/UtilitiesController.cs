using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Api.Logging;

namespace Ipstset.WhosMcu.Api.Controllers
{
    [Route("utilities")]
    [ApiController]
    public class UtilitiesController : BaseController
    {
        [Route("requestlog")]
        [HttpGet]
        public ActionResult<RequestLog> RequestLog()
        {
            var log = Ipstset.WhosMcu.Api.Logging.RequestLog.Create(RouteData, HttpContext);
            return log;
        }
    }
}
