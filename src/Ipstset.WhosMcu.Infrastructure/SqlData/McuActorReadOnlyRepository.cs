using Dapper;
using Ipstset.WhosMcu.Application;
using Ipstset.WhosMcu.Application.McuActors;
using Ipstset.WhosMcu.Application.McuActors.SearchMcuActors;
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
    public class McuActorReadOnlyRepository : IMcuActorReadOnlyRepository
    {
        private DbSettings _db;
        public McuActorReadOnlyRepository(DbSettings settings)
        {
            _db = settings;
        }

        public async Task<QueryResult<McuActorResponse>> GetMcuActorsAsync(SearchMcuActorsRequest request)
        {
            var actors = new List<McuActorResponse>();
            var sql = $"exec {_db.Schema}.search_json @table, @searchTerm";
            using (var sqlConnection = new SqlConnection(_db.Connection))
            {
                var documents = await sqlConnection.QueryAsync<SqlDocument>(sql, new { table = "mcu_actor", searchTerm = request.Name });
                actors.AddRange(documents.Select(document => document.ToMcuActorResponse()).Where(response => response != null));
            }

            //sort
            var sorter = new Sorter<McuActorResponse>();
            actors = sorter.Sort(actors, request.Sort?.ToArray()).ToList();

            return new QueryResult<McuActorResponse> { Items = actors.Take(request.Limit), TotalRecords = actors.Count, Limit = request.Limit };
        }
    }
}
