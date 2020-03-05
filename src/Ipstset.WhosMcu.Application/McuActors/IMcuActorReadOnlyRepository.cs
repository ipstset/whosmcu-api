using Ipstset.WhosMcu.Application.McuActors.SearchMcuActors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Application.McuActors
{
    public interface IMcuActorReadOnlyRepository
    {
        Task<QueryResult<McuActorResponse>> GetMcuActorsAsync(SearchMcuActorsRequest request);
    }
}
