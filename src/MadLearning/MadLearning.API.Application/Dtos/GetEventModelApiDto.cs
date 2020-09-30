using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record GetEventModelApiDto(
        string Id,
        string Name,
        string Description,
        DateTimeOffset Time,
        string? ImageUrl,
        string? ImageAlt,
        string? Location,
        PersonModelApiDto? Owner,
        IEnumerable<PersonModelApiDto>? Participants);
}
