using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Domain.Entities;
using MadLearning.API.Infrastructure.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Persistence
{
    internal sealed partial class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<EventModelDbDto> collection;

        public EventRepository(EventDbSettings eventDbSettings, MongoClient client)
        {
            var database = client.GetDatabase(eventDbSettings.DatabaseName);

            this.collection = database.GetCollection<EventModelDbDto>(eventDbSettings.EventCollectionName);
        }

        public async Task<EventModel?> GetEvent(string eventId, CancellationToken cancellationToken)
        {
            var cursor = await this.collection.FindAsync(e => e.Id == eventId, cancellationToken: cancellationToken);

            var @event = await cursor.SingleOrDefaultAsync(cancellationToken);

            if (@event is null)
                return null;

            return @event.ToEventModel();
        }

        public async Task<List<EventModel>> GetEvents(EventFilterApiDto eventFilter, CancellationToken cancellationToken)
        {
            var filter = CreateFilter(eventFilter);

            var events = this.collection.Find(filter).SortBy(e => e.StartTime).Limit(eventFilter.Limit);

            var dtos = await events.ToListAsync(cancellationToken);

            return dtos
                .Select(dto => dto.ToEventModel())
                .ToList();

            static FilterDefinition<EventModelDbDto> CreateFilter(EventFilterApiDto eventFilter)
            {
                var filter1 = Builders<EventModelDbDto>.Filter.Lte(nameof(EventModelDbDto.StartTime), eventFilter.To);
                var filter2 = Builders<EventModelDbDto>.Filter.Gte(nameof(EventModelDbDto.StartTime), eventFilter.From);

                return Builders<EventModelDbDto>.Filter.And(filter1, filter2);
            }
        }

        public async Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            var dbDto = eventModel.ToDbDto();
            await this.collection.InsertOneAsync(dbDto, cancellationToken: cancellationToken);

            eventModel = dbDto.ToEventModel();

            return eventModel;
        }

        public async Task DeleteEvent(string id, CancellationToken cancellationToken)
        {
            await this.collection.DeleteOneAsync(ev => ev.Id == id, cancellationToken);
        }

        public async Task UpdateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            await this.collection.ReplaceOneAsync(ev => ev.Id == eventModel.Id, eventModel.ToDbDto(), cancellationToken: cancellationToken);
        }
    }
}
