using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Application.Movies.GetMovies;

namespace Ipstset.WhosMcu.Application.Movies
{
    public interface IMovieReadOnlyRepository
    {
        Task<MovieResponse> GetByIdAsync(string id);
        Task<QueryResult<MovieResponse>> GetMoviesAsync(GetMoviesRequest request);
    }
}
