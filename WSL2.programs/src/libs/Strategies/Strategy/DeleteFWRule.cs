using NetFwTypeLib;
using Firewall;

namespace Strategies
{
    public class DeleteFWRule : IStrategies
    {
        private IFirewall _rules;
        public DeleteFWRule(IFirewall firewall) 
        { 
            _rules = firewall.BuildOutbound().BuildOutbound();
        }

        public void Execute()
        {
            Console.WriteLine("Removing firewall rules");

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));


            foreach (var firewallRule in _rules.Elements) {
                if (firewallPolicy.Rules.Equals(firewallRule)) {
                    try {
                        firewallPolicy.Rules.Remove(firewallRule.Name);
                    } catch (UnauthorizedAccessException exception) {
                        Console.WriteLine($"Cannot remove firewall rule, You must run command as Administrator, message: {exception.Message}");
                        return;
                    }

                    Console.WriteLine($"Rule has been removed: {firewallRule.Name}");
                    return;
                }

                Console.WriteLine($"No firewall rule with given name: {firewallRule.Name}");
            }
        }
    }
}
