using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Queries
{
    public sealed record GetEvents(EventFilterApiDto dto) : IRequest<List<GetEventModelApiDto>>;

    internal sealed record GetEventsQueryHandler(IEventRepository repository) : IRequestHandler<GetEvents, List<GetEventModelApiDto>>
    {
        public async Task<List<GetEventModelApiDto>> Handle(GetEvents request, CancellationToken cancellationToken)
        {
            try
            {
                var eventModels = await this.repository.GetEvents(request.dto, cancellationToken);

                return eventModels.Select(dto => dto.ToApiDto()).ToList();
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
