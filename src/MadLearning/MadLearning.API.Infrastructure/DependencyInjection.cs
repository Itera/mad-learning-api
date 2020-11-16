using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Application.Service;
using MadLearning.API.Application.Services;
using MadLearning.API.Infrastructure.Configuration;
using MadLearning.API.Infrastructure.Persistence;
using MadLearning.API.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polly;
using Polly.Extensions.Http;
using System;

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
                services.AddSingleton(sp => new MongoClient(sp.GetRequiredService<EventDbSettings>().ConnectionString));
                services.AddSingleton<IEventRepository, EventRepository>();
                services.AddSingleton<IIdGenerator, IdGenerator>();
                services.AddTransient<ICalendarService, CalendarService>();

                services.Configure<SlackOptions>(
                        configuration.GetSection(nameof(SlackOptions)));
                services.AddHttpClient<SlackChatMesssageService>()
                    .Services
                    .AddPolicyRegistry()
                    .Add("Retries", HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
                services.AddTransient<IChatMessageService>(sp => sp.GetRequiredService<SlackChatMesssageService>());

                // Add Hangfire services
                services.AddHangfire((sp, configuration) => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMongoStorage(sp.GetRequiredService<MongoClient>(), $"{sp.GetRequiredService<EventDbSettings>().DatabaseName}hangfire", new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                            BackupStrategy = new CollectionMongoBackupStrategy(),
                        },
                        CheckConnection = true,
                    }));

                services.AddHangfireServer(serverOptions =>
                {
                    serverOptions.ServerName = "MadLearning Hangfire server";
                });

                services.AddSingleton<IJobScheduler, HangfireJobScheduler>();

                services.AddSingleton<ITimeZoneService, TimeZoneService>();
            }

            return services;
        }
    }
}
