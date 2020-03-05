using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.McuActors.SearchMcuActors
{
    public class SearchMcuActorsRequest: IRequest<QueryResult<McuActorResponse>>
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public IEnumerable<SortItem> Sort { get; set; }
    }
}
