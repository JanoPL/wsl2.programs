using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ports;

namespace Strategies
{
    public static class StrategiesServiceCollectionExtensions
    {
        public static IServiceCollection AddStrategies(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddScoped<IContext, Context>();
            services.AddPorts();

            return services;
        }

        public static IServiceCollection AddStrategies(
            this IServiceCollection services
        )
        {
            services.AddScoped<IContext, Context>();
            services.AddPorts();

            return services;
        }
    }
}
