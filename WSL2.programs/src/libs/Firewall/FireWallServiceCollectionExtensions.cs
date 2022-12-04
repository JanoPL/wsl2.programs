using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WSL;

namespace Firewall
{
    public static class FireWallServiceCollectionExtensions
    {
        public static IServiceCollection AddFirewall(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            return services;
        }

        public static IServiceCollection AddFirewallDependency(
            this IServiceCollection services
        ) {
            services.AddScoped<IFirewall, Rules>();
            services.TryAddScoped<IWsl, Wsl>();

            return services; 
        }
    }
}
