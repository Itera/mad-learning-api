using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record DeleteEvent(string Id) : IRequest;

    internal sealed record DeleteEventCommandHandler(IEventRepository repository, ICurrentUserService userService) : IRequestHandler<DeleteEvent>
    {
        public async Task<Unit> Handle(DeleteEvent request, CancellationToken cancellationToken)
        {
            try
            {
                var eventModel = await this.repository.GetEvent(request.Id, cancellationToken);

                if (eventModel is null)
                    throw new EventException("Event with that ID doesn't exist");

                var currentUser = this.userService.GetUserInfo();
                if (currentUser.Id != eventModel.Owner?.Id)
                    throw new EventException("User not authorized to delete this event");

                await this.repository.DeleteEvent(request.Id, cancellationToken);
                return Unit.Value;
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
