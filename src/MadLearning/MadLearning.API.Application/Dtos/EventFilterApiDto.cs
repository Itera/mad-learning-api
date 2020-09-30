using System;

namespace MadLearning.API.Application.Dtos
{
    public record EventFilterApiDto
    {
        public DateTimeOffset From { get; init; } = DateTimeOffset.MinValue;

        public DateTimeOffset To { get; init; } = DateTimeOffset.MaxValue;

        public int Limit { get; init; } = 100;
    }
}
