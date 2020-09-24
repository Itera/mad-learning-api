using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public sealed class EventModelDbDto
    {
        public string? Id { get; init; }

        public DateTimeOffset Time { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public PersonModelDbDto? Owner { get; set; }

        public List<PersonModelDbDto> Participants { get; init; } = new List<PersonModelDbDto>();
    }
}
