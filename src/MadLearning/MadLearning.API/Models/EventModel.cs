using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MadLearning.API.Models
{
    public class EventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public DateTimeOffset Time { get; set;  }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public PersonModel Owner { get; set; }

        public List<PersonModel> Participants { get; set; }

    }
}
