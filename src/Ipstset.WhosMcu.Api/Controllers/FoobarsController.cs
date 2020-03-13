using Ipstset.WhosMcu.Api.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Controllers
{
    [Route("foobars")]
    [EnableCors("CorsPolicy")]
    [ServiceFilter(typeof(ApiTokenServiceFilter))]
    public class FoobarsController : BaseController
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "foo", "bar" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            //return id.ToString();
            return AppUser.SessionId;
        }
    }
}
