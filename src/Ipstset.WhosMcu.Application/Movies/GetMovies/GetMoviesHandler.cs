using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Application.Movies.GetMovies
{
    public class GetMoviesHandler : IRequestHandler<GetMoviesRequest, QueryResult<MovieResponse>>
    {
        private IMovieReadOnlyRepository _readOnlyRepository;

        public GetMoviesHandler(IMovieReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<QueryResult<MovieResponse>> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
        {
            return await _readOnlyRepository.GetMoviesAsync(request);
        }
    }
}
