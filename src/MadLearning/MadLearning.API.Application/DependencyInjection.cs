using MadLearning.API.Application.Common.Behaviours;
using MadLearning.API.Application.HostedServices;
using MadLearning.API.Application.Jobs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MadLearning.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Mediator pattern
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            // Hosted services
            services.AddHostedService<SeedService>();

            services.AddHostedService<SchedulerService>();

            // Jobs
            services.AddTransient<ChatMessageEventNotificationJob>();

            return services;
        }
    }
}
