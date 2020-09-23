using MadLearning.API.Config;
using MadLearning.API.Dtos;
using MadLearning.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Repositories
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

        public async Task<List<GetEventModelApiDto>> GetEvents(EventFilter eventFilter, CancellationToken cancellationToken)
        {
            var filter = CreateFilter(eventFilter);

            var events = await this.collection.FindAsync(filter, cancellationToken: cancellationToken);

            var dtos = await events.ToListAsync(cancellationToken);

            return dtos
                .Select(dto => GetEventModelApiDto.FromModel(EventModel.FromDbDto(dto)))
                .ToList();

            static FilterDefinition<EventModelDbDto> CreateFilter(EventFilter eventFilter)
            {
                var filter1 = Builders<EventModelDbDto>.Filter.Lte("Time", eventFilter.To);
                var filter2 = Builders<EventModelDbDto>.Filter.Gte("Time", eventFilter.From);

                return Builders<EventModelDbDto>.Filter.And(filter1, filter2);
            }
        }

        public async Task<GetEventModelApiDto> CreateEvent(CreateEventModelApiDto createEventModel, CancellationToken cancellationToken)
        {
            var eventModel = EventModel.Create(createEventModel);

            await this.collection.InsertOneAsync(EventModelDbDto.FromModel(eventModel), cancellationToken: cancellationToken);

            return GetEventModelApiDto.FromModel(eventModel);
        }

        public async Task DeleteEvent(DeleteEventModelApiDto deleteEventModel, CancellationToken cancellationToken)
        {
            await this.collection.DeleteOneAsync(ev => ev.Id == deleteEventModel.Id, cancellationToken);
        }

        public async Task UpdateEvent(UpdateEventModelApiDto updateEventModel, CancellationToken cancellationToken)
        {
            var model = EventModel.Update(updateEventModel);

            await this.collection.ReplaceOneAsync(ev => ev.Id == updateEventModel.Id, EventModelDbDto.FromModel(model), cancellationToken: cancellationToken);
        }
    }
}
