using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Application.McuActors.SearchMcuActors
{
    public class SearchMcuActorsHandler : IRequestHandler<SearchMcuActorsRequest, QueryResult<McuActorResponse>>
    {
        private IMcuActorReadOnlyRepository _readOnlyRepository;

        public SearchMcuActorsHandler(IMcuActorReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<QueryResult<McuActorResponse>> Handle(SearchMcuActorsRequest request, CancellationToken cancellationToken)
        {
            return await _readOnlyRepository.GetMcuActorsAsync(request);
        }
    }
}
