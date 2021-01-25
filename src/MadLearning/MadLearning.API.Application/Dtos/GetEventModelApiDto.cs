using MadLearning.API.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MadLearning.API.Application.Dtos
{
    public record GetEventModelApiDto(
        string Id,
        string Name,
        string Description,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        string? ImageUrl,
        string? ImageAlt,
        string? Location,
        string EventType,
        PersonModelApiDto? Owner,
        IEnumerable<PersonModelApiDto>? Participants);
}
