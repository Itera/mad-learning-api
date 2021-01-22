using System;

namespace MadLearning.API.Application.Dtos
{
    public record UpdateEventModelApiDto(
        string Name,
        string Description,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        string? ImageUrl,
        string? ImageAlt,
        string? Location);
}
