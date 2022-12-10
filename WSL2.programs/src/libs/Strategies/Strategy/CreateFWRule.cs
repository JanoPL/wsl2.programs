using System.Runtime.Versioning;
using Firewall;
using NetFwTypeLib;

namespace Strategies
{
    [SupportedOSPlatform("windows")]
    public class CreateFWRule : IStrategies
    {
        private const string ProgID = "HNetCfg.FwPolicy2";
        private readonly IFirewall _firewall;
        public CreateFWRule(IFirewall firewall)
        {
            _firewall = firewall;
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
                            Console.WriteLine($"Cannot add firewall rule, You must run command as Administrator, message: {exception.Message}");
                            return;
                        }

                        Console.WriteLine($"Rule has been created: {firewallRule.Name}");
                    }
                }
            }
        }
    }
}
