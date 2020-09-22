using MadLearning.API.Config;
using MadLearning.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadLearning.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<EventModel> collection;

        public EventRepository(EventDbSettings eventDbSettings)
        {
            var client = new MongoClient(eventDbSettings.ConnectionString);
            var database = client.GetDatabase(eventDbSettings.DatabaseName);

            collection = database.GetCollection<EventModel>(eventDbSettings.EventCollectionName);
        }

        public async Task<EventModel> CreateEvent(EventModel eventModel)
        {
            await collection.InsertOneAsync(eventModel);

            return eventModel;
        }

        public async Task DeleteEvent(EventModel eventModel)
        {
            await collection.DeleteOneAsync(ev => ev.Id == eventModel.Id);
        }

        public async Task<List<EventModel>> GetEvents(EventFilter eventFilter)
        {

            var id1 = MongoDB.Bson.ObjectId.GenerateNewId();
            var id2 = MongoDB.Bson.ObjectId.GenerateNewId();

            var filter = CreateFilter(eventFilter);

            var events = await collection.FindAsync(filter);

            return await events.ToListAsync();
        }

        public async Task UpdateEvent(EventModel eventModel)
        {
            await collection.ReplaceOneAsync(ev => ev.Id == eventModel.Id, eventModel);
        }

        FilterDefinition<EventModel> CreateFilter(EventFilter eventFilter)
        {
            var filter1 = Builders<EventModel>.Filter.Lte("Time", eventFilter.To);
            var filter2 = Builders<EventModel>.Filter.Gte("Time", eventFilter.From);

            return Builders<EventModel>.Filter.And(filter1, filter2);
        }
    }
}
