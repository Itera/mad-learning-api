using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record UpdateEvent(string Id, UpdateEventModelApiDto dto) : IRequest;

    internal sealed record UpdateEventCommandHandler(IEventRepository repository, ICurrentUserService currentUserService/*, ICalendarService calendarService*/) : IRequestHandler<UpdateEvent>
    {
        public async Task<Unit> Handle(UpdateEvent request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = this.currentUserService.GetUserInfo();

                var existingEventModel = await this.repository.GetEvent(request.Id, cancellationToken);

                if (existingEventModel is null)
                    throw new Exception("Event doesn't exist");

                if (existingEventModel.Owner?.Id != currentUser.Id)
                    throw new Exception("User is not owner of this event");

                var isUpdate = existingEventModel.Update(
                    request.dto.Name,
                    request.dto.Description,
                    request.dto.StartTime,
                    request.dto.EndTime,
                    request.dto.ImageUrl,
                    request.dto.ImageAlt,
                    request.dto.Location,
                    request.dto.EventType);

                if (!isUpdate)
                    return Unit.Value;

                // This method of updating is inefficient and not concurrency safe.
                // Atm only owners can update, so concurrency not a problem.
                // If we should tolerate concurrency we need transaction or similar.
                await this.repository.UpdateEvent(existingEventModel, cancellationToken);

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
