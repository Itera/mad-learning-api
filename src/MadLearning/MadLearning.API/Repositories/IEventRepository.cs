using MadLearning.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadLearning.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetEvents(EventFilter eventFilter);

        Task<EventModel> CreateEvent(EventModel eventModel);

        Task DeleteEvent(EventModel eventModel);

        Task UpdateEvent(EventModel eventModel);
    }
}
