using System;

namespace MadLearning.API.Models
{
    public record EventFilter
    {
        public DateTimeOffset From { get; init; } = DateTimeOffset.MinValue;

        public DateTimeOffset To { get; init; } = DateTimeOffset.MaxValue;
    }
}
