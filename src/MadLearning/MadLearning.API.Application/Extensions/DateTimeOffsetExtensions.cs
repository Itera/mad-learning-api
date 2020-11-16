using System;

namespace MadLearning.API.Application.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset TruncateHours(this DateTimeOffset src) => src.AddHours(-src.Hour);

        public static DateTimeOffset TruncateMinutes(this DateTimeOffset src) => src.AddMinutes(-src.Minute);

        public static DateTimeOffset TruncateSeconds(this DateTimeOffset src) => src.AddSeconds(-src.Second);

        public static DateTimeOffset TruncateTime(this DateTimeOffset src) => src
            .TruncateHours()
            .TruncateMinutes()
            .TruncateSeconds();
    }
}
