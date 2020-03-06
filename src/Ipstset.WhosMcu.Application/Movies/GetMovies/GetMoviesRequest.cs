using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.Movies.GetMovies
{
    public class GetMoviesRequest: IRequest<QueryResult<MovieResponse>>
    {
        public int Limit { get; set; }
        public string StartAfter { get; set; }
        public IEnumerable<SortItem> Sort { get; set; }
    }
}
