using System.Runtime.Versioning;
using Firewall;
using Microsoft.Extensions.Logging;
using NetFwTypeLib;

namespace Strategies
{
    [SupportedOSPlatform("windows")]
    public class CreateFWRule : IStrategies
    {
        private const string ProgID = "HNetCfg.FwPolicy2";
        private readonly IFirewall _rules;
        private readonly ILogger _logger;

        public CreateFWRule(IFirewall firewall, ILogger logger)
        {
            _rules = firewall.BuildInbound().BuildOutbound();
            _logger = logger;
        }

        public void Execute()
        {
            Type? type = Type.GetTypeFromProgID(ProgID);
            if (type != null) {
                if (Activator.CreateInstance(type) is INetFwPolicy2 firewallPolicy) {
                    if (_rules.Elements.Count== 0) {
                        _logger.LogWarning("There are no windows firewall rules, rules will not be applied");
                        return;
                    }

                    foreach (var firewallRule in _rules.Elements) {
                        try {
                            firewallPolicy.Rules.Add(firewallRule);
                        } catch (UnauthorizedAccessException exception) {
                            _logger.LogError("Cannot firewall rule, You must run command as Administrator, message: {message}", exception.Message);
                            return;
                        } catch (Exception exception) {
                            _logger.LogError("Cannot add firewall rule, message: {message}", exception.Message);
                            return;
                        }

                        _logger.LogInformation("Rule has been created: {name}", firewallRule.Name);
                    }
                }
            }
        }
    }
}
