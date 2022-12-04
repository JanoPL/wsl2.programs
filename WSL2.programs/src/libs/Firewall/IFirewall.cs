using NetFwTypeLib;

namespace Firewall
{
    public interface IFirewall
    {
        public Rules BuildInbound();
        public Rules BuildOutbound();
        public IList<INetFwRule> Elements { get; set; }
    }
}
