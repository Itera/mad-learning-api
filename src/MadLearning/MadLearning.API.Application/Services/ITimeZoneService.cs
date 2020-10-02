using System;

namespace MadLearning.API.Application.Services
{
    public interface ITimeZoneService
    {
        TimeZoneInfo GetTimeZoneForIANAName(string ianaName);
    }
}
