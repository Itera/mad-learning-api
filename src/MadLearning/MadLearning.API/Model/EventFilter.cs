using System;

namespace MadLearning.API.Model
{

    public class EventFilter
    {
        public DateTimeOffset From { get; set; } = DateTimeOffset.MinValue;

        public DateTimeOffset To { get; set; } = DateTimeOffset.MaxValue;
    }
}
