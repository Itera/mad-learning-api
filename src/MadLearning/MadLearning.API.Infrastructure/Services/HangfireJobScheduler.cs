using Hangfire;
using Hangfire.Common;
using MadLearning.API.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Infrastructure.Services
{
    internal sealed class HangfireJobScheduler : IJobScheduler
    {
        private readonly IRecurringJobManager jobManager;

        public HangfireJobScheduler(IRecurringJobManager jobManager)
        {
            this.jobManager = jobManager;
        }

        public void ScheduleJob<TJob>(string cronExpression, TimeZoneInfo timeZone)
            where TJob : IJob
        {
            this.jobManager.AddOrUpdate(typeof(TJob).FullName, Job.FromExpression<TJob>(static j => j.Execute()), cronExpression, timeZone);
        }
    }
}
