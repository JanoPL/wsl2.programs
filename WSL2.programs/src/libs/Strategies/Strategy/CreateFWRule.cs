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
        private readonly IFirewall _firewall;
        private readonly ILogger _logger;

        public CreateFWRule(IFirewall firewall, ILogger logger)
        {
            _firewall = firewall;
            _logger = logger;
        }

        public void Execute()
        {
            Type? type = Type.GetTypeFromProgID(ProgID);
            if (type != null) {
                if (Activator.CreateInstance(type) is INetFwPolicy2 firewallPolicy) {
                    foreach (var firewallRule in _firewall.Elements) {
                        try {
                            firewallPolicy.Rules.Add(firewallRule);
                        } catch (UnauthorizedAccessException exception) {
                            _logger.LogError("Cannot add firewall rule, You must run command as Administrator, message: {message}", exception.Message);
                            return;
                        }

                        _logger.LogInformation("Rule has been created: {name}", firewallRule.Name);
                    }
                }
            }
        }
    }
}
