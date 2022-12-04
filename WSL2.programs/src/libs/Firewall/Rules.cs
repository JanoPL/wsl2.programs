using NetFwTypeLib;
using WSL;

namespace Firewall
{
    public class Rules : IFirewall
    {
        readonly private IWsl _wsl;

        private IList<INetFwRule> _elements = new List<INetFwRule>();

        public IList<INetFwRule> Elements { get => _elements; set => _elements = value; }

        public Rules(IWsl wsl)
        {
            _wsl = wsl;
        }

        public Rules BuildInbound()
        {
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            InboundRule inboundRule = new(_wsl);

            firewallRule.Name = inboundRule.Name;
            firewallRule.Action = inboundRule.Action;
            firewallRule.Protocol = (int)inboundRule.Protocol;
            firewallRule.LocalPorts = inboundRule.LocalPorts;
            firewallRule.Direction = inboundRule.Direction;
            firewallRule.Enabled = inboundRule.Enabled;
            firewallRule.InterfaceTypes = inboundRule.InterfaceTypes;

            Elements.Add(firewallRule);

            return this;
        }

        public Rules BuildOutbound()
        {
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            OutboundRule outboundRule = new(_wsl);

            firewallRule.Name = outboundRule.Name;
            firewallRule.Action = outboundRule.Action;
            firewallRule.Protocol = (int)outboundRule.Protocol;
            firewallRule.LocalPorts = outboundRule.LocalPorts;
            firewallRule.Direction = outboundRule.Direction;
            firewallRule.Enabled = outboundRule.Enabled;
            firewallRule.InterfaceTypes = outboundRule.InterfaceTypes;

            Elements.Add(firewallRule);

            return this;
        }
    }
}
