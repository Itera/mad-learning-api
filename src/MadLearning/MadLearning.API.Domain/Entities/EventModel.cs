using System;
using System.Collections.Generic;
using System.Linq;

namespace MadLearning.API.Domain.Entities
{
    public sealed class EventModel
    {
        public EventModel(string id, string name, string description, DateTimeOffset time, string? imageUrl, PersonModel? owner, IEnumerable<PersonModel>? participants)
        {
            this.Id = id ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Name = name ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Description = description ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Time = time;
            this.ImageUrl = imageUrl;
            this.Owner = owner;
            this.Participants = participants?.ToList() ?? new ();
        }

        public EventModel(string name, string description, DateTimeOffset time, string? imageUrl, PersonModel? owner, IEnumerable<PersonModel>? participants)
        {
            this.Id = string.Empty;
            this.Name = name;
            this.Description = description;
            this.Time = time;
            this.ImageUrl = imageUrl;
            this.Owner = owner;
            this.Participants = participants?.ToList() ?? new ();
        }

        public string Id { get; }

        public DateTimeOffset Time { get; set;  }

        public string Name { get; init; }

        public string Description { get; init; }

        public string? ImageUrl { get; init; }

        public PersonModel? Owner { get; set; }

        public List<PersonModel> Participants { get; init; } = new List<PersonModel>();

        public static EventModel Create(string name, string description, DateTimeOffset time, string? imageUrl, PersonModel owner)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(name, description, time, imageUrl, owner, null);

            return @event;
        }

        public static EventModel Update(string id, string name, string description, DateTimeOffset time, string? imageUrl, PersonModel? owner, IEnumerable<PersonModel>? participants)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Event id is null or whitespace");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(id, name, description, time, imageUrl, owner, participants);

            return @event;
        }
    }
}
