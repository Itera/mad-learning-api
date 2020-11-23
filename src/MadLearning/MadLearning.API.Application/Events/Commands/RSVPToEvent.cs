using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record RSVPToEvent(string Id, string Email, string FirstName, string LastName) : IRequest;

    internal sealed record RSVPToEventCommandHandler(IEventRepository repository) : IRequestHandler<RSVPToEvent>
    {
        public async Task<Unit> Handle(RSVPToEvent request, CancellationToken cancellationToken)
        {
            try
            {
                await this.repository.RSVPToEvent(request.Id, request.Email, request.FirstName, request.LastName, cancellationToken);
                return Unit.Value;
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
