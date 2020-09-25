using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Commands
{
    public sealed record DeleteEvent(DeleteEventModelApiDto dto) : IRequest;

    internal sealed record DeleteEventCommandHandler(IEventRepository repository) : IRequestHandler<DeleteEvent>
    {
        public async Task<Unit> Handle(DeleteEvent request, CancellationToken cancellationToken)
        {
            await this.repository.DeleteEvent(request.dto.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
