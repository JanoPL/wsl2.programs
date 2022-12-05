using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }

        public static IServiceCollection AddStrategies(
            this IServiceCollection services
        )
        {
            services.AddScoped<IContext, Context>();

            return services;
        }
    }
}
