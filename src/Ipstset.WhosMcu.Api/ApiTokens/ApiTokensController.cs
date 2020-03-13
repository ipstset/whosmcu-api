using Ipstset.WhosMcu.Api.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.ApiTokens
{
    [Route("tokens")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [HttpException]
    public class ApiTokensController: BaseController
    {
        private IApiTokenManager _apiTokenManager;
        public ApiTokensController(IApiTokenManager apiTokenManager)
        {
            _apiTokenManager = apiTokenManager;
        }
        /// <summary>
        /// Create api token
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = Constants.Routes.Tokens.CreateToken)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<string>> Post()
        {
            var token = _apiTokenManager.CreateToken();
            return Json(new { token });
        }
    }
}
