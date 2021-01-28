using MadLearning.API.Application.Dtos;
using MadLearning.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Persistence
{
    public interface IEventRepository
    {
        Task<EventModel?> GetEvent(string eventId, CancellationToken cancellationToken);

        Task<List<EventModel>> GetEvents(EventFilterApiDto eventFilter, CancellationToken cancellationToken);

        Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken);

        Task DeleteEvent(string id, CancellationToken cancellationToken);

        Task UpdateEvent(EventModel eventModel, CancellationToken cancellationToken);

        Task JoinEvent(string id, string userId, string email, string firstName, string lastName, CancellationToken cancellationToken);

        Task DropEvent(string id, string userId, string email, CancellationToken cancellationToken);
    }
}
