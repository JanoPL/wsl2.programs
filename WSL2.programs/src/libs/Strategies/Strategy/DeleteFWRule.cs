using System.Runtime.Versioning;
using Firewall;
using Microsoft.Extensions.Logging;
using NetFwTypeLib;

namespace Strategies
{
    [SupportedOSPlatform("windows")]
    public class DeleteFWRule : IStrategies
    {
        private const string ProgID = "HNetCfg.FwPolicy2";
        private readonly IFirewall _rules;
        private readonly ILogger _logger;

        public DeleteFWRule(IFirewall firewall, ILogger logger)
        {
            _rules = firewall.BuildOutbound().BuildOutbound();
            _logger = logger;
        }

        public void Execute()
        {
            Console.WriteLine("Removing firewall rules");

            Type? type = Type.GetTypeFromProgID(ProgID);

            if (type != null) {
                INetFwPolicy2? firewallPolicy = Activator.CreateInstance(type) as INetFwPolicy2;

                if (firewallPolicy != null) {
                    foreach (var firewallRule in _rules.Elements) {
                        if (firewallPolicy.Rules.Equals(firewallRule)) {
                            try {
                                firewallPolicy.Rules.Remove(firewallRule.Name);
                            } catch (UnauthorizedAccessException exception) {
                                _logger.LogError("Cannot remove firewall rule, You must run command as Administrator, message: {message}", exception.Message);
                                return;
                            }

                            _logger.LogInformation("Rule has been removed: {name}", firewallRule.Name);
                            return;
                        }

                        _logger.LogInformation("No firewall rule with given name: {name}", firewallRule.Name);
                    }
                }
            }
        }
    }
}
