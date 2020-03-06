using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Application.Actors.GetActors
{
    public class GetActorsHandler : IRequestHandler<GetActorsRequest, QueryResult<ActorResponse>>
    {
        private IActorReadOnlyRepository _readOnlyRepository;

        public GetActorsHandler(IActorReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }
        public async Task<QueryResult<ActorResponse>> Handle(GetActorsRequest request, CancellationToken cancellationToken)
        {
            return await _readOnlyRepository.GetActorsAsync(request);
        }
    }
}
