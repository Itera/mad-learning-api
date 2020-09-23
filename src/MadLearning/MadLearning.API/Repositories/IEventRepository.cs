using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MadLearning.API.Dtos;
using MadLearning.API.Models;

namespace MadLearning.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<GetEventModelApiDto>> GetEvents(EventFilter eventFilter, CancellationToken cancellationToken);

        Task<GetEventModelApiDto> CreateEvent(CreateEventModelApiDto eventModel, CancellationToken cancellationToken);

        Task DeleteEvent(DeleteEventModelApiDto eventModel, CancellationToken cancellationToken);

        Task UpdateEvent(UpdateEventModelApiDto eventModel, CancellationToken cancellationToken);
    }
}
