using MadLearning.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MadLearning.API.Dtos
{
    public class EventModelDbDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }

        [Required]
        public DateTimeOffset Time { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public PersonModel? Owner { get; set; }

        public List<PersonModel> Participants { get; init; } = new List<PersonModel>();

        public static EventModelDbDto FromModel(EventModel? model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            return new EventModelDbDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Owner = model.Owner,
                Participants = model.Participants,
                Time = model.Time,
            };
        }
    }
}
