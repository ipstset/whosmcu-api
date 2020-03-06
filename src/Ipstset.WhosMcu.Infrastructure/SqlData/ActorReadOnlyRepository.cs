using Dapper;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.Actors;
using Ipstset.WhosMcu.Application.Actors.GetActors;
using Ipstset.WhosMcu.Infrastructure.Extensions;
using Ipstset.WhosMcu.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Infrastructure.SqlData
{
    public class ActorReadOnlyRepository: IActorReadOnlyRepository
    {
        private DbSettings _db;
        public ActorReadOnlyRepository(DbSettings settings)
        {
            _db = settings;
        }

        public Task<ActorResponse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryResult<ActorResponse>> GetActorsAsync(GetActorsRequest request)
        {
            //query process:
            //1. get all json from starting point
            //2. filter unauthorized records
            //3. apply remaining request filters
            //4. take # of records based on limit
            var actors = new List<ActorResponse>();
            var sql = $"exec {_db.Schema}.get_json_all @table,@startAfter";
            using (var sqlConnection = new SqlConnection(_db.Connection))
            {
                var documents = await sqlConnection.QueryAsync<SqlDocument>(sql, new { table = "actor", startAfter = request.StartAfter });
                foreach (var document in documents)
                {
                    var response = document.ToActorResponse();
                    if (response != null)
                        actors.Add(response);
                }
            }

            //sort
            var sorter = new Sorter<ActorResponse>();
            actors = sorter.Sort(actors, request.Sort?.ToArray()).ToList();

            return new QueryResult<ActorResponse> { Items = actors.Take(request.Limit), TotalRecords = actors.Count, Limit = request.Limit, StartAfter = request.StartAfter };
        }
    }
}
