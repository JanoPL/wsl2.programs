using System.Runtime.Versioning;
using Firewall;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Strategies;
using WSL;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace Portproxy
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var host = BuildHost(args).Build();

            IApp app = host.Services.GetRequiredService<IApp>();

            app.Run(args);
        }

        private static IHostBuilder BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices)
                .UseSerilog();

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IApp, App>();
            services.AddSingleton<IAppConfig, AppConfig>();
            services.AddSingleton<IContext, Context>();
            services.AddSingleton<IWsl, Wsl>();
            services.AddSingleton<IFirewall, Rules>();
        }
    }
}