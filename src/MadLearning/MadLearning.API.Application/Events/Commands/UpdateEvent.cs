using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record UpdateEvent(UpdateEventModelApiDto dto) : IRequest;

    internal sealed record UpdateEventCommandHandler(IEventRepository repository) : IRequestHandler<UpdateEvent>
    {
        public async Task<Unit> Handle(UpdateEvent request, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Update(
               request.dto.Id,
               request.dto.Name,
               request.dto.Description,
               request.dto.Time,
               request.dto.ImageUrl,
               request.dto.Owner.ToPersonModel(),
               request.dto.Participants.ToPersonModels());

            await this.repository.UpdateEvent(eventModel, cancellationToken);

            return Unit.Value;
        }
    }
}
