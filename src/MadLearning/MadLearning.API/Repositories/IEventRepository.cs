using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MadLearning.API.Models;

namespace MadLearning.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetEvents(EventFilter eventFilter, CancellationToken cancellationToken);

        Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken);

        Task DeleteEvent(EventModel eventModel, CancellationToken cancellationToken);

        Task UpdateEvent(EventModel eventModel, CancellationToken cancellationToken);
    }
}
