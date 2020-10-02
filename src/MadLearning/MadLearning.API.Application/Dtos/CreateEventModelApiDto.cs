using System;

namespace MadLearning.API.Application.Dtos
{
    public record CreateEventModelApiDto(
        string Name,
        string Description,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        string? ImageUrl,
        string? ImageAlt,
        string? Location,
        string? EventType,
        PersonModelApiDto Owner);
}
