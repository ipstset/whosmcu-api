using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Api.Attributes;
using Ipstset.WhosMcu.Api.Helpers;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.Actors;
using Ipstset.WhosMcu.Application.Actors.GetActors;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Ipstset.WhosMcu.Api.Actors
{
    [Route("actors")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [HttpException]
    public class ActorsController : BaseController
    {
        private readonly IMediator _mediator;
        public ActorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all actors matching supplied criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QueryResult<ActorResponse>>> Get([FromQuery] GetActorsModel model)
        {
            return await _mediator.Send(new GetActorsRequest
            {
                Limit = model.Limit ?? Constants.MaxRequestLimit,
                StartAfter = model.StartAfter,
                Sort = model.Sort.ToSortItems("FullName")
            });
        }

        ///// <summary>
        ///// Get actor by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}", Name = Constants.Routes.Actors.GetActor)]
        //[ProducesResponseType(StatusCodes.Status200OK)]

        //public async Task<ActionResult<ActorResponse>> Get([FromRoute]string id)
        //{
        //    return await _mediator.Send(new GetActorRequest { Id = id });
        //}
    }
}
