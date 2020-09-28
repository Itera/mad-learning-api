using MadLearning.API.Application.Dtos;
using MadLearning.API.Application.Persistence;
using MadLearning.API.Infrastructure.Configuration;
using MadLearning.API.Infrastructure.Persistence;
using MadLearning.API.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

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

            {
                // Register BSON class maps
                BsonClassMap.RegisterClassMap<EventModelDbDto>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
                BsonClassMap.RegisterClassMap<PersonModelDbDto>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
            }

            return services;
        }
    }
}
