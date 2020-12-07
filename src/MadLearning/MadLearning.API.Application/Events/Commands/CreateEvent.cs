using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Service;
using MadLearning.API.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record CreateEvent(CreateEventModelApiDto dto) : IRequest<GetEventModelApiDto?>;

    internal sealed record CreateEventCommandHandler(IEventRepository repository, ICalendarService calendarService) : IRequestHandler<CreateEvent, GetEventModelApiDto?>
    {
        public async Task<GetEventModelApiDto?> Handle(CreateEvent request, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Create(
                    request.dto.Name,
                    request.dto.Description,
                    request.dto.StartTime,
                    request.dto.EndTime,
                    request.dto.ImageUrl,
                    request.dto.ImageAlt,
                    request.dto.Location,
                    request.dto.Owner.ToPersonModel()!);

            EventModel? createdEvent = null;

            try
            {
                createdEvent = await this.repository.CreateEvent(eventModel, cancellationToken);

                await this.calendarService.AddEvent(createdEvent);

                return createdEvent.ToApiDto();
            }
            catch (StorageException e)
            {
                //this.logger.LogError("Could not create Event");

                throw new EventException(e.Message, e);
            }
            catch (CalendarException e)
            {
                //this.logger.LogError("Could not access Calendar");

                if (createdEvent is { })
                    await this.repository.DeleteEvent(createdEvent.Id, cancellationToken);

                throw new EventException(e.Message, e);
            }
        }
    }
}
