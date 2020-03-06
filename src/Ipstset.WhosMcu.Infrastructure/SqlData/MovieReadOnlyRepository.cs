using Dapper;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.Movies;
using Ipstset.WhosMcu.Application.Movies.GetMovies;
using Ipstset.WhosMcu.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipstset.WhosMcu.Infrastructure.Extensions;

namespace Ipstset.WhosMcu.Infrastructure.SqlData
{
    public class MovieReadOnlyRepository : IMovieReadOnlyRepository
    {
        private DbSettings _db;
        public MovieReadOnlyRepository(DbSettings settings)
        {
            _db = settings;
        }

        public Task<MovieResponse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryResult<MovieResponse>> GetMoviesAsync(GetMoviesRequest request)
        {
            //query process:
            //1. get all json from starting point
            //2. filter unauthorized records
            //3. apply remaining request filters
            //4. take # of records based on limit
            var movies = new List<MovieResponse>();
            var sql = $"exec {_db.Schema}.get_json_all @table,@startAfter";
            using (var sqlConnection = new SqlConnection(_db.Connection))
            {
                var documents = await sqlConnection.QueryAsync<SqlDocument>(sql, new { table = "movie", startAfter = request.StartAfter });
                foreach (var document in documents)
                {
                    var response = document.ToMovieResponse();
                    if (response != null)
                        movies.Add(response);
                }
            }

            //sort
            var sorter = new Sorter<MovieResponse>();
            movies = sorter.Sort(movies, request.Sort?.ToArray()).ToList();

            return new QueryResult<MovieResponse> { Items = movies.Take(request.Limit), TotalRecords = movies.Count, Limit = request.Limit, StartAfter = request.StartAfter };
        }
    }
}
