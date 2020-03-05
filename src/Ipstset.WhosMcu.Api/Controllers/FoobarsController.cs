using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Controllers
{
    [Route("foobars")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class FoobarsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "foo", "bar" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id.ToString();
        }
    }
}
