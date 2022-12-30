using System.Runtime.Versioning;
using CommandLine;
using Firewall;
using Microsoft.Extensions.Logging;
using Ports;
using Strategies;
using Strategies.Strategy;
using WSL;

namespace Portproxy
{
    [SupportedOSPlatform("windows")]
    public class App : IApp
    {
        private readonly IContext _context;
        private readonly IFirewall _firewall;
        private readonly IWsl _wsl;
        private readonly ILogger<App> _logger;
        private readonly IPorts _ports;

        public App(
            ILogger<App> logger,
            IContext context,
            IFirewall firewall,
            IWsl wsl,
            IPorts ports
        ) { 
            _context = context;
            _firewall = firewall;
            _wsl = wsl;
            _logger = logger;
            _ports = ports;

            _logger.LogDebug("Application constructed");
        }

        public void Run(string[] args)
        {
            _logger.LogInformation("Application: {application_name} Running", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            _context.AddStrategy(new CheckUserRoles(_logger));
            _context.ExecuteStrategies();
            _context.CleanStrategies();

            var results = Parser
                .Default
                .ParseArguments<AppConfig>(args)
                .WithParsed<AppConfig>(o => {
                    if (o.List) {
                        _context.AddStrategy(new List(_logger));
                        _context.ExecuteStrategies();
                    } else if (o.ListPorts) {
                        _context.AddStrategy(new GetWslPorts(_logger, _ports));
                        _context.ExecuteStrategies();
                    } else if (o.ListIpAddress) {
                        _context.AddStrategy(new CheckWslIPAddress(_logger, _wsl));
                        _context.ExecuteStrategies();
                    } else if (o.Delete) {
                        _context.AddStrategy(new DeleteFWRule(_firewall, _logger));
                        _context.ExecuteStrategies();
                    } else if (o.Create) {
                        _context.AddStrategy(new CreateFWRule(_firewall, _logger));
                        _context.AddStrategy(new CheckWslIPAddress(_logger, _wsl));
                        _context.AddStrategy(new CheckWslPorts(_logger, _wsl));
                        _context.AddStrategy(new AddPortProxyInformation(_wsl, _logger));
                        _context.ExecuteStrategies();
                    }
                });
        }
    }
}
