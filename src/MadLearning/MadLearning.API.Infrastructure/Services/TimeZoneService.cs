using MadLearning.API.Application.Services;
using System;
using TimeZoneConverter;

namespace MadLearning.API.Infrastructure.Services
{
    internal sealed class TimeZoneService : ITimeZoneService
    {
        public TimeZoneInfo GetTimeZoneForIANAName(string ianaName)
        {
            return TZConvert.GetTimeZoneInfo(ianaName);
        }
    }
}
