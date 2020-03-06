using Ipstset.WhosMcu.Api.Attributes;
using Ipstset.WhosMcu.Api.Helpers;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.Movies;
using Ipstset.WhosMcu.Application.Movies.GetMovies;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Movies
{
    [Route("movies")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [HttpException]
    public class MoviesController: BaseController
    {
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all movies matching supplied criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QueryResult<MovieResponse>>> Get([FromQuery] GetMoviesModel model)
        {
            return await _mediator.Send(new GetMoviesRequest
            {
                Limit = model.Limit ?? Constants.MaxRequestLimit,
                StartAfter = model.StartAfter,
                Sort = model.Sort.ToSortItems("ReleaseDate")
            });
        }

        ///// <summary>
        ///// Get movie by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}", Name = Constants.Routes.Movies.GetMovie)]
        //[ProducesResponseType(StatusCodes.Status200OK)]

        //public async Task<ActionResult<MovieResponse>> Get([FromRoute]string id)
        //{
        //    return await _mediator.Send(new GetMovieRequest { Id = id });
        //}
    }
}
