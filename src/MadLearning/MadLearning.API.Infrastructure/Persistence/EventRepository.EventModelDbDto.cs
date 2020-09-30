using MadLearning.API.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Infrastructure.Persistence
{
    internal sealed partial class EventRepository
    {
        public sealed class EventModelDbDto
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; init; }

            public DateTimeOffset Time { get; set; }

            public string? Name { get; set; }

            public string? Description { get; set; }

            public string? ImageUrl { get; set; }

            public PersonModelDbDto? Owner { get; set; }

            public List<PersonModelDbDto> Participants { get; init; } = new List<PersonModelDbDto>();

            public EventModel ToEventModel()
            {
                if (this.Id is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");
                if (this.Name is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");
                if (this.Description is null)
                    throw new InvalidOperationException("Event can only be created from valid DB dto");

                return new EventModel(
                    this.Id,
                    this.Name,
                    this.Description,
                    this.Time,
                    this.ImageUrl,
                    this.Owner?.ToPersonModel(),
                    this.Participants.Select(d => d.ToPersonModel()));
            }
        }
    }

    internal static class EventModelDbDtoMappingExtensions
    {
        public static EventRepository.EventModelDbDto ToDbDto(this EventModel? model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            return new EventRepository.EventModelDbDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Owner = model.Owner.ToDbDto(),
                Participants = model.Participants.ToDbDtos()?.ToList() ?? new (),
                Time = model.Time,
            };
        }
    }
}
