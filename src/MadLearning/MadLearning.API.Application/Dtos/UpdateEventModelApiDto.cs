using MadLearning.API.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record UpdateEventModelApiDto(
        string Id,
        string Name,
        string Description,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        string? ImageUrl,
        string? ImageAlt,
        string? Location,
        EventType EventType,
        PersonModelApiDto? Owner,
        IEnumerable<PersonModelApiDto>? Participants);
}
