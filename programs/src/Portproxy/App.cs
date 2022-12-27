using Microsoft.Extensions.Logging;
using CommandLine;
using Firewall;
using Strategies;
using WSL;
using System.Runtime.Versioning;
using Strategies.Strategy;

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
            _logger.LogInformation("Application: {application_name} Running", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            context.AddStrategy(new CheckUserRoles(_logger));
            context.ExecuteStrategies();
            context.CleanStrategies();

            var results = Parser
                .Default
                .ParseArguments<AppConfig>(args)
                .WithParsed<AppConfig>(o => {
                    if (o.List) {
                        context.AddStrategy(new List(_logger));
                        context.ExecuteStrategies();
                    } else if (o.ListPorts) {
                        context.AddStrategy(new GetWslPorts(_logger));
                        context.ExecuteStrategies();
                    } else if (o.ListIpAddress) {
                        context.AddStrategy(new CheckWslIPAddress(_logger, wsl));
                        context.ExecuteStrategies();
                    } else if (o.Delete) {
                        context.AddStrategy(new DeleteFWRule(firewall, _logger));
                        context.ExecuteStrategies();
                    } else if (o.Create) {
                        context.AddStrategy(new CreateFWRule(firewall, _logger));
                        context.AddStrategy(new CheckWslIPAddress(_logger, wsl));
                        context.AddStrategy(new CheckWslPorts(_logger, wsl));
                        context.AddStrategy(new AddPortProxyInformation(wsl, _logger));
                        context.ExecuteStrategies();
                    }
                });
        }
    }
}
