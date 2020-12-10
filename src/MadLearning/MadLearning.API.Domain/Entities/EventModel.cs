using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Domain.Entities
{
    public sealed class EventModel
    {
        public EventModel(
            string id,
            string? calendarId,
            string name,
            string description,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            string? imageUrl,
            string? imageAlt,
            string? location,
            PersonModel? owner,
            IEnumerable<PersonModel>? participants)
        {
            this.Id = id ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.CalendarId = calendarId;
            this.Name = name ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Description = description ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ImageUrl = imageUrl;
            this.ImageAlt = imageAlt;
            this.Location = location;
            this.Owner = owner;
            this.Participants = participants?.ToList() ?? new ();
        }

        public EventModel(
            string name,
            string description,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            string? imageUrl,
            string? imageAlt,
            string? location,
            PersonModel? owner,
            IEnumerable<PersonModel>? participants)
        {
            this.Id = string.Empty;
            this.CalendarId = string.Empty;
            this.Name = name;
            this.Description = description;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ImageUrl = imageUrl;
            this.ImageAlt = imageAlt;
            this.Location = location;
            this.Owner = owner;
            this.Participants = participants?.ToList() ?? new ();
        }

        public string Id { get; }

        public string? CalendarId { get; set; }

        public DateTimeOffset StartTime { get; set;  }

        public DateTimeOffset EndTime { get; set;  }

        public string Name { get; init; }

        public string Description { get; init; }

        public string? ImageUrl { get; init; }

        public string? ImageAlt { get; init; }

        public string? Location { get; init; }

        public PersonModel? Owner { get; set; }

        public List<PersonModel> Participants { get; init; } = new List<PersonModel>();

        public static EventModel Create(
            string name,
            string description,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            string? imageUrl,
            string? imageAlt,
            string? location,
            PersonModel owner)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(name, description, startTime, endTime, imageUrl, imageAlt, location, owner, null);

            return @event;
        }

        public static EventModel Update(
            string id,
            string calendarId,
            string name,
            string description,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            string? imageUrl,
            string? imageAlt,
            string? location,
            PersonModel? owner,
            IEnumerable<PersonModel>? participants)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Event id is null or whitespace");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(id, calendarId, name, description, startTime, endTime, imageUrl, imageAlt, location, owner, participants);

            return @event;
        }
    }
}
