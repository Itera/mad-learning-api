using Microsoft.Extensions.DependencyInjection;

namespace MadLearning.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
