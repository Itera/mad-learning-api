using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MadLearning.API.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record UpdateEvent(UpdateEventModelApiDto dto) : IRequest;

    internal sealed record UpdateEventCommandHandler(IEventRepository repository, ICurrentUserService currentUserService/*, ICalendarService calendarService*/) : IRequestHandler<UpdateEvent>
    {
        public async Task<Unit> Handle(UpdateEvent request, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Update(
                request.dto.Id,
                request.dto.CalendarId,
                request.dto.CalendarUid,
                request.dto.Name,
                request.dto.Description,
                request.dto.StartTime,
                request.dto.EndTime,
                request.dto.ImageUrl,
                request.dto.ImageAlt,
                request.dto.Location,
                request.dto.Owner.ToPersonModel(),
                request.dto.Participants.ToPersonModels());

            try
            {
                var currentUser = this.currentUserService.GetUserInfo();

                await this.repository.UpdateEvent(eventModel, cancellationToken);

                var refreshPage = await this.repository.GetEvent(request.dto.Id, cancellationToken);

                if (refreshPage is null)
                    throw new EventException("Could not get event from database");

                //await this.calendarService.UpdateEvent(eventModel, cancellationToken);
                return Unit.Value;
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
