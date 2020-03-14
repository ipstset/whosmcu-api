using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Ipstset.WhosMcu.Api.Logging
{
    public class LogRepository : ILogRepository
    {
        private string _connection;

        public LogRepository(string connection)
        {
            _connection = connection;
        }

        public async Task Save(RequestLog requestLog)
        {

            var sql = $"exec request_log_insert @logDate,@parameters,@route";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                await sqlConnection.ExecuteAsync(sql, new
                {
                    requestLog.LogDate, 
                    requestLog.Parameters, 
                    requestLog.Route
                });
            }

        }
    }
}
