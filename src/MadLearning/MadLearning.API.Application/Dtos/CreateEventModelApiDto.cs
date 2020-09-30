using System;

namespace MadLearning.API.Application.Dtos
{
    public record CreateEventModelApiDto(
        string Name,
        string Description,
        DateTimeOffset Time,
        string? ImageUrl,
        string? ImageAlt,
        string? Location,
        PersonModelApiDto Owner);
}
