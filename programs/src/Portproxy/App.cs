using Microsoft.Extensions.Logging;
using CommandLine;
using Firewall;
using Microsoft.Extensions.Hosting;
using Strategies;
using WSL;
using System.Runtime.Versioning;

namespace Portproxy
{
    [SupportedOSPlatform("windows")]
    public class App : IApp
    {
        private readonly IContext context;
        private readonly IFirewall firewall;
        private readonly IWsl wsl;
        private readonly ILogger<App> _logger;

        public App(
            ILogger<App> logger,
            IContext context,
            IFirewall firewall,
            IWsl wsl
        ) {
            this.context = context;
            this.firewall = firewall;
            this.wsl = wsl;
            _logger = logger;
            _logger.LogDebug("Application constructed");
        }

        public void Run(string[] args)
        {
            _logger.LogInformation("Application Run");

            var results = Parser
                .Default
                .ParseArguments<AppConfig>(args)
                .WithParsed<AppConfig>(o => {
                    if (o.List) {
                        context.AddStrategy(new List(_logger));
                        context.ExecuteStrategies();
                    } else if (o.Delete) {
                        context.AddStrategy(new DeleteFWRule(firewall, _logger));
                        context.ExecuteStrategies();
                    } else if (o.Create) {
                        context.AddStrategy(new CreateFWRule(firewall, _logger));
                        context.AddStrategy(new CheckWslIPAddress(_logger, wsl));
                        context.AddStrategy(new AddPortProxyInformation(wsl, _logger));
                        context.ExecuteStrategies();
                    }
                });
        }
    }
}
