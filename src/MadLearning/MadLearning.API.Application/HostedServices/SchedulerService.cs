using MadLearning.API.Application.Jobs;
using MadLearning.API.Application.Services;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.HostedServices
{
    internal sealed class SchedulerService : IHostedService
    {
        private readonly IJobScheduler jobScheduler;
        private readonly IHostEnvironment hostEnvironment;
        private readonly ITimeZoneService timeZoneService;

        public SchedulerService(IJobScheduler jobScheduler, IHostEnvironment hostEnvironment, ITimeZoneService timeZoneService)
        {
            this.jobScheduler = jobScheduler;
            this.hostEnvironment = hostEnvironment;
            this.timeZoneService = timeZoneService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (this.hostEnvironment.IsProduction())
            {
                var timeZone = this.timeZoneService.GetTimeZoneForIANAName("Europe/Oslo");

                // Every day at 15:00: https://crontab.guru/#0_15_*_*_*
                this.jobScheduler.ScheduleJob<ChatMessageEventNotificationJob>("0 15 * * *", timeZone);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
