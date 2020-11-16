using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Events.Queries
{
    public sealed record GetEvent(string Id) : IRequest<GetEventModelApiDto>;

    internal sealed record GetEventQueryHandler(IEventRepository repository) : IRequestHandler<GetEvent, GetEventModelApiDto?>
    {
        public async Task<GetEventModelApiDto?> Handle(GetEvent request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                throw new ArgumentException("Null or whitespace Event ID");

            try
            {
                var dto = await this.repository.GetEvent(request.Id, cancellationToken);

                if (dto is null)
                    return null;

                return dto.ToApiDto();
            }
            catch (StorageException e)
            {
                throw new EventException(e.Message, e);
            }
        }
    }
}
