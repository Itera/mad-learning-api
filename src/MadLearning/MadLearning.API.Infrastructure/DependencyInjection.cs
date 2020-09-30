using MadLearning.API.Application.Persistence;
using MadLearning.API.Infrastructure.Configuration;
using MadLearning.API.Infrastructure.Persistence;
using MadLearning.API.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadLearning.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            {
                // Configuration
                services.Configure<EventDbSettingsDto>(
                        configuration.GetSection(nameof(EventDbSettings)));

                services.AddSingleton<EventDbSettings>(static sp =>
                        sp.GetRequiredService<IOptions<EventDbSettingsDto>>().Value);
            }

            {
                // Dependencies
                services.AddSingleton<IEventRepository, EventRepository>();
                services.AddSingleton<Application.Services.IIdGenerator, IdGenerator>();
            }

            return services;
        }
    }
}
