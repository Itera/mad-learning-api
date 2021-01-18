using MadLearning.API.Application.Services;
using MadLearning.API.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Services
{
    internal sealed class NoopCalendarService : ICalendarService
    {
        public Task<(string? EventId, string? EventUid)> AddEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            return Task.FromResult((default(string), default(string)));
        }

        public Task RsvpToEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
