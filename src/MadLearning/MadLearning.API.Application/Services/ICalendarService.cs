using MadLearning.API.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Services
{
    public interface ICalendarService
    {
        Task<(string? EventId, string? EventUid)> AddEvent(EventModel eventModel, CancellationToken cancellationToken);

        Task RsvpToEvent(EventModel eventModel, CancellationToken cancellationToken);
    }
}
