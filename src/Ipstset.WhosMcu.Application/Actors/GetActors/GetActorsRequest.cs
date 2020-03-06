using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.Actors.GetActors
{
    public class GetActorsRequest:IRequest<QueryResult<ActorResponse>>
    {
        public int Limit { get; set; }
        public string StartAfter { get; set; }
        public IEnumerable<SortItem> Sort { get; set; }
    }
}
