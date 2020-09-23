using MadLearning.API.Application.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Persistence
{
    public interface IEventRepository
    {
        Task<GetEventModelApiDto?> GetEvent(string eventId, CancellationToken cancellationToken);

        Task<List<GetEventModelApiDto>> GetEvents(EventFilterApiDto eventFilter, CancellationToken cancellationToken);

        Task<GetEventModelApiDto> CreateEvent(CreateEventModelApiDto eventModel, CancellationToken cancellationToken);

        Task DeleteEvent(DeleteEventModelApiDto eventModel, CancellationToken cancellationToken);

        Task UpdateEvent(UpdateEventModelApiDto eventModel, CancellationToken cancellationToken);
    }
}
