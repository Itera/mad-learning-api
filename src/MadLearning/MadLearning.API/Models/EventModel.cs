using MadLearning.API.Dtos;
using System;
using System.Collections.Generic;

namespace MadLearning.API.Models
{
    public sealed class EventModel
    {
        private EventModel(EventModelDbDto dto)
        {
            this.Id = dto.Id ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Name = dto.Name ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
            this.Description = dto.Description ?? throw new InvalidOperationException("Event can only be created from valid DB dto");
        }

        private EventModel(string name, string description)
        {
            this.Id = string.Empty;
            this.Name = name;
            this.Description = description;
        }

        public string Id { get; }

        public DateTimeOffset Time { get; set;  }

        public string Name { get; init; }

        public string Description { get; init; }

        public PersonModel? Owner { get; set; }

        public List<PersonModel> Participants { get; init; } = new List<PersonModel>();

        public static EventModel FromDbDto(EventModelDbDto? dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            return new EventModel(dto);
        }

        public static EventModel Create(CreateEventModelApiDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(dto.Name, dto.Description);

            return @event;
        }

        public static EventModel Update(UpdateEventModelApiDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Event name is null or whitespace");
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Event description is null or whitespace");

            var @event = new EventModel(dto.Name, dto.Description);

            return @event;
        }
    }
}
