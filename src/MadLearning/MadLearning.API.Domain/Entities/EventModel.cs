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
            string? calendarUid,
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
            this.CalendarUid = calendarUid;
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
            this.CalendarUid = string.Empty;
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

        public string? CalendarUid { get; set; }

        public DateTimeOffset StartTime { get; private set;  }

        public DateTimeOffset EndTime { get; private set;  }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public string? ImageAlt { get; private set; }

        public string? Location { get; private set; }

        public PersonModel? Owner { get; set; }

        public List<PersonModel> Participants { get; init; } = new List<PersonModel>();

        public bool Update(
            string name,
            string description,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            string? imageUrl,
            string? imageAlt,
            string? location)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is null or whitespace", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description is null or whitespace", nameof(name));

            if (startTime < DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(startTime), "Starttime cannot be in the past");
            if (endTime < DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(startTime), "Endtime cannot be in the past");
            if (startTime == endTime)
                throw new ArgumentException("Starttime and endtime cannot be the same value", $"{nameof(startTime)}|{nameof(endTime)}");

            var isUpdate =
                name != this.Name ||
                description != this.Description ||
                startTime != this.StartTime ||
                endTime != this.EndTime ||
                imageUrl != this.ImageUrl ||
                imageAlt != this.ImageAlt ||
                location != this.Location;

            this.Name = name;
            this.Description = description;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ImageUrl = imageUrl;
            this.ImageAlt = imageAlt;
            this.Location = location;

            return isUpdate;
        }

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
    }
}
