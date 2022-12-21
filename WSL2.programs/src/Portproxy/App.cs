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
        private readonly ILogger<App> logger;

        public App(
            ILogger<App> logger,
            IContext context,
            IFirewall firewall,
            IWsl wsl
        ) {
            this.context = context;
            this.firewall = firewall;
            this.wsl = wsl;
            logger.Log(LogLevel.Debug, "Application constructed");
        }

        public class Options
        {
            [Option('l', "List", Required = false, HelpText = "show portproxy information")]
            public bool List { get; set; }

            [Option('d', "Delete", Required = false, HelpText = "Delete all portproxy information")]
            public bool Delete { get; set; }

            [Option('c', "Create", Required = false, HelpText = "Create all portproxy information")]
            public bool Create { get; set; }
        }

        public void Run(string[] args)
        { 
            Parser
                .Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(o => {
                    if (o.List) {
                        context.AddStrategy(new List());
                        context.ExecuteStrategies();
                    } else if (o.Delete) {
                        context.AddStrategy(new DeleteFWRule(firewall));
                        context.ExecuteStrategies();
                    } else if (o.Create) {
                        context.AddStrategy(new CreateFWRule(firewall));
                        context.AddStrategy(new CheckWslIPAddress(logger, wsl));
                        context.AddStrategy(new AddPortProxyInformation(wsl));
                        context.ExecuteStrategies();
                    }
                });
        }
    }
}
