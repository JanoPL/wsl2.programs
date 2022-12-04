namespace WSL
{
    public interface IWsl
    {
        public Settings Settings { get; }

        public IWsl SetIpAddress(string IpAddress);
        public IWsl SetPort(int port);
        public IWsl SetPorts(IEnumerable<string> ports);
    }
}