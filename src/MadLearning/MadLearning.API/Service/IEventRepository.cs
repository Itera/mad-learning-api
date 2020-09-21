using MadLearning.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadLearning.API.Service
{
    public interface IEventRepository
    {
        Task<List<EventModel>> GetEvents(EventFilter eventFilter);

        Task<EventModel> CreateEvent(EventModel eventModel);

        Task DeleteEvent(EventModel eventModel);

        Task UpdateEvent(EventModel eventModel);
    }
}
