using NetFwTypeLib;
using WSL;

namespace Firewall
{
    internal class OutboundRule
    {
        readonly private IWsl _wsl;
        public string Name { get; set; } = "JANOPL WSL2 firewall unlock ports outbound";
        public NET_FW_ACTION_ Action { get; set; } = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
        public NET_FW_IP_PROTOCOL_ Protocol { get; set; } = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
        public string LocalPorts { get; set; }
        public NET_FW_RULE_DIRECTION_ Direction { get; set; } = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
        public bool Enabled { get; set; } = true;
        public string InterfaceTypes { get; set; } = "All";

        public OutboundRule(IWsl wsl)
        {
            _wsl = wsl;
            LocalPorts = String.Join(',', _wsl.Settings.Ports);
        }
    }
}
