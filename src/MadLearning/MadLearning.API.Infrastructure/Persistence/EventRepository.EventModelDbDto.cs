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

            public string? CalendarId { get; set; }

            public string? CalendarUid { get; set; }

            [BsonRepresentation(BsonType.String)]
            public DateTimeOffset StartTime { get; set; }

            [BsonRepresentation(BsonType.String)]
            public DateTimeOffset EndTime { get; set; }

            public string? Name { get; set; }

            public string? Description { get; set; }

            public string? ImageUrl { get; set; }

            public string? ImageAlt { get; set; }

            public string? Location { get; set; }

            public EventType EventType { get; set; }

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
                    this.CalendarId,
                    this.CalendarUid,
                    this.Name,
                    this.Description,
                    this.StartTime,
                    this.EndTime,
                    this.ImageUrl,
                    this.ImageAlt,
                    this.Location,
                    this.EventType,
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
                CalendarId = model.CalendarId,
                CalendarUid = model.CalendarUid,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                ImageAlt = model.ImageAlt,
                Location = model.Location,
                EventType = model.EventType,
                Owner = model.Owner.ToDbDto(),
                Participants = model.Participants.ToDbDtos()?.ToList() ?? new (),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };
        }
    }
}
