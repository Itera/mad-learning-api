using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record CreateEventModelApiDto(string Name, string Description, DateTimeOffset Time, PersonModelApiDto? Owner, IEnumerable<PersonModelApiDto>? Participants);
}
