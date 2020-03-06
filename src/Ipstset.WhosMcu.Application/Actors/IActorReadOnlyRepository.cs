using Ipstset.WhosMcu.Application.Actors.GetActors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Application.Actors
{
    public interface IActorReadOnlyRepository
    {
        Task<QueryResult<ActorResponse>> GetActorsAsync(GetActorsRequest request);
    }
}
