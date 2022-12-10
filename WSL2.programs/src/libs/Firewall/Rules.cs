using System.Runtime.Versioning;
using Microsoft.TeamFoundation.Common;
using NetFwTypeLib;
using WSL;

namespace Firewall
{
    [SupportedOSPlatform("windows")]
    public class Rules : IFirewall
    {
        private const string ProgID = "HNetCfg.FWRule";
        readonly private IWsl _wsl;

        private IList<INetFwRule> _elements = new List<INetFwRule>();

        public IList<INetFwRule> Elements { get => _elements; set => _elements = value; }

        public Rules(IWsl wsl)
        {
            _wsl = wsl;
        }

        public Rules BuildInbound()
        {
            Type? type = Type.GetTypeFromProgID(ProgID);

            if (type != null) {
                INetFwRule? firewallRule = Activator.CreateInstance(type) as INetFwRule;

                if (firewallRule != null) {
                    InboundRule inboundRule = new(_wsl);

                    firewallRule.Name = inboundRule.Name;
                    firewallRule.Action = inboundRule.Action;
                    firewallRule.Protocol = (int)inboundRule.Protocol;
                    firewallRule.LocalPorts = inboundRule.LocalPorts;
                    firewallRule.Direction = inboundRule.Direction;
                    firewallRule.Enabled = inboundRule.Enabled;
                    firewallRule.InterfaceTypes = inboundRule.InterfaceTypes;

                    Elements.Add(firewallRule);
                }
            }

            return this;
        }

        public Rules BuildOutbound()
        {
            var type = Type.GetTypeFromProgID(ProgID);

            if (type != null) {
                INetFwRule? firewallRule = Activator.CreateInstance(type) as INetFwRule;

                if (firewallRule != null) {
                    OutboundRule outboundRule = new(_wsl);

                    firewallRule.Name = outboundRule.Name;
                    firewallRule.Action = outboundRule.Action;
                    firewallRule.Protocol = (int)outboundRule.Protocol;
                    firewallRule.LocalPorts = outboundRule.LocalPorts;
                    firewallRule.Direction = outboundRule.Direction;
                    firewallRule.Enabled = outboundRule.Enabled;
                    firewallRule.InterfaceTypes = outboundRule.InterfaceTypes;

                    Elements.Add(firewallRule);
                }
            }

            return this;
        }
    }
}
