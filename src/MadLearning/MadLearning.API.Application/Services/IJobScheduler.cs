using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Services
{
    public interface IJobScheduler
    {
        void ScheduleJob<TJob>(string cronExpression, TimeZoneInfo timeZone)
            where TJob : IJob;
    }
}
