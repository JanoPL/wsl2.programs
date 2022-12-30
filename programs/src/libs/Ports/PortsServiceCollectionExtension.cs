using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ports
{
    public static class PortsServiceCollectionExtension
    {
        public static IServiceCollection AddPorts(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddScoped<IPorts, PortsService>();

            return services;
        }

        public static IServiceCollection AddPorts(
            this IServiceCollection services
        )
        {
            services.AddScoped<IPorts, PortsService>();

            return services;
        }
    }
}
