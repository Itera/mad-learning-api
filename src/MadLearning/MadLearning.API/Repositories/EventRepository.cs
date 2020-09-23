using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MadLearning.API.Config;
using MadLearning.API.Models;
using MongoDB.Driver;

namespace MadLearning.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<EventModel> collection;

        public EventRepository(EventDbSettings eventDbSettings)
        {
            var client = new MongoClient(eventDbSettings.ConnectionString);
            var database = client.GetDatabase(eventDbSettings.DatabaseName);

            this.collection = database.GetCollection<EventModel>(eventDbSettings.EventCollectionName);
        }

        public async Task<EventModel> CreateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            await this.collection.InsertOneAsync(eventModel, cancellationToken: cancellationToken);

            return eventModel;
        }

        public async Task DeleteEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            await this.collection.DeleteOneAsync(ev => ev.Id == eventModel.Id, cancellationToken);
        }

        public async Task<List<EventModel>> GetEvents(EventFilter eventFilter, CancellationToken cancellationToken)
        {
            var filter = CreateFilter(eventFilter);

            var events = await this.collection.FindAsync(filter, cancellationToken: cancellationToken);

            return await events.ToListAsync();

            static FilterDefinition<EventModel> CreateFilter(EventFilter eventFilter)
            {
                var filter1 = Builders<EventModel>.Filter.Lte("Time", eventFilter.To);
                var filter2 = Builders<EventModel>.Filter.Gte("Time", eventFilter.From);

                return Builders<EventModel>.Filter.And(filter1, filter2);
            }
        }

        public async Task UpdateEvent(EventModel eventModel, CancellationToken cancellationToken)
        {
            await this.collection.ReplaceOneAsync(ev => ev.Id == eventModel.Id, eventModel, cancellationToken: cancellationToken);
        }
    }
}
