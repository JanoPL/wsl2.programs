using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WSL
{
    public static class WslServiceCollectionExtensions
    {
        public static IServiceCollection AddWsl(
            this IServiceCollection services,
            IConfiguration config
        ) {
            return services;
        } 

        public static IServiceCollection AddWslDependency(
            this IServiceCollection services)
        {
            services.AddScoped<IWsl, Wsl>();

            return services;
        }
    }
}
