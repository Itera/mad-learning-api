using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Mapping;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Domain.Entities;
using MadLearning.API.Infrastructure.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Persistence
{
    internal sealed class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<EventModelDbDto> collection;

        public EventRepository(EventDbSettings eventDbSettings)
        {
            var client = new MongoClient(eventDbSettings.ConnectionString);
            var database = client.GetDatabase(eventDbSettings.DatabaseName);

            this.collection = database.GetCollection<EventModelDbDto>(eventDbSettings.EventCollectionName);
        }

        public async Task<GetEventModelApiDto?> GetEvent(string eventId, CancellationToken cancellationToken)
        {
            var cursor = await this.collection.FindAsync(e => e.Id == eventId, cancellationToken: cancellationToken);

            var @event = await cursor.SingleOrDefaultAsync(cancellationToken);

            if (@event is null)
                return null;

            return @event.ToEventModel().ToApiDto();
        }

        public async Task<List<GetEventModelApiDto>> GetEvents(EventFilterApiDto eventFilter, CancellationToken cancellationToken)
        {
            var filter = CreateFilter(eventFilter);

            var events = await this.collection.FindAsync(filter, cancellationToken: cancellationToken);

            var dtos = await events.ToListAsync(cancellationToken);

            return dtos
                .Select(dto => dto.ToEventModel().ToApiDto())
                .ToList();

            static FilterDefinition<EventModelDbDto> CreateFilter(EventFilterApiDto eventFilter)
            {
                var filter1 = Builders<EventModelDbDto>.Filter.Lte(nameof(EventModelDbDto.Time), eventFilter.To);
                var filter2 = Builders<EventModelDbDto>.Filter.Gte(nameof(EventModelDbDto.Time), eventFilter.From);

                return Builders<EventModelDbDto>.Filter.And(filter1, filter2);
            }
        }

        public async Task<GetEventModelApiDto> CreateEvent(CreateEventModelApiDto createEventModel, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Create(
                createEventModel.Name,
                createEventModel.Description,
                createEventModel.Time,
                createEventModel.Owner.ToPersonModel(),
                createEventModel.Participants.ToPersonModels());

            var dbDto = eventModel.ToDbDto();
            await this.collection.InsertOneAsync(dbDto, cancellationToken: cancellationToken);

            eventModel = dbDto.ToEventModel();

            return eventModel.ToApiDto();
        }

        public async Task DeleteEvent(DeleteEventModelApiDto deleteEventModel, CancellationToken cancellationToken)
        {
            await this.collection.DeleteOneAsync(ev => ev.Id == deleteEventModel.Id, cancellationToken);
        }

        public async Task UpdateEvent(UpdateEventModelApiDto updateEventModel, CancellationToken cancellationToken)
        {
            var model = EventModel.Update(
                updateEventModel.Id,
                updateEventModel.Name,
                updateEventModel.Description,
                updateEventModel.Time,
                updateEventModel.Owner.ToPersonModel(),
                updateEventModel.Participants.ToPersonModels());

            await this.collection.ReplaceOneAsync(ev => ev.Id == model.Id, model.ToDbDto(), cancellationToken: cancellationToken);
        }
    }
}
