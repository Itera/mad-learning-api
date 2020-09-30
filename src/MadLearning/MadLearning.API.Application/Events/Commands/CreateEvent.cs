using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record CreateEvent(CreateEventModelApiDto dto) : IRequest<GetEventModelApiDto>;

    internal sealed record CreateEventCommandHandler(IEventRepository repository) : IRequestHandler<CreateEvent, GetEventModelApiDto>
    {
        public async Task<GetEventModelApiDto> Handle(CreateEvent request, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Create(
                    request.dto.Name,
                    request.dto.Description,
                    request.dto.Time,
                    request.dto.ImageUrl,
                    request.dto.ImageAlt,
                    request.dto.Location,
                    request.dto.Owner.ToPersonModel()!);

            var createdEvent = await this.repository.CreateEvent(eventModel, cancellationToken);

            return createdEvent.ToApiDto();
        }
    }
}
