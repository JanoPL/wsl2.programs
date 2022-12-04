namespace WSL
{
    public interface IWsl
    {
        public Settings Settings { get; }

        public IWsl SetIpAddress(string IpAddress);
        public IWsl AddPort(int port);
        public IWsl AddPort(string port);
        public IWsl SetPorts(IList<string> ports);
    }
}