using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record RSVPToEvent(string Id) : IRequest;

    internal sealed record RSVPToEventCommandHandler(IEventRepository repository, ICurrentUserService currentUserService, ICalendarService calendarService) : IRequestHandler<RSVPToEvent>
    {
        public async Task<Unit> Handle(RSVPToEvent request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = this.currentUserService.GetUserInfo();

                await this.repository.RSVPToEvent(request.Id, currentUser.Email, currentUser.FirstName, currentUser.LastName, cancellationToken);

                var eventModel = await this.repository.GetEvent(request.Id, cancellationToken);

                if (eventModel is null)
                    throw new EventException("Could not get event from database");

                await this.calendarService.RsvpToEvent(eventModel, cancellationToken);
                return Unit.Value;
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
