using Ipstset.WhosMcu.Api.Attributes;
using Ipstset.WhosMcu.Api.Helpers;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.McuActors;
using Ipstset.WhosMcu.Application.McuActors.SearchMcuActors;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.McuActors
{
    [Route("mcuactors")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [HttpException]
    public class McuActorsController
    {
        private readonly IMediator _mediator;
        public McuActorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all MCU actors matching supplied criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QueryResult<McuActorResponse>>> Get([FromQuery] SearchMcuActorsModel model)
        {
            return await _mediator.Send(new SearchMcuActorsRequest
            {
                Name = model.Name,
                Limit = model.Limit ?? Constants.MaxRequestLimit,
                Sort = model.Sort.ToSortItems("FullName")
            });
        }
    }
}
