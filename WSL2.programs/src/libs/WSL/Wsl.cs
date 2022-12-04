namespace WSL
{
    public class Wsl : IWsl
    {
        private Settings _settings;
        public Settings Settings
        {
            get
            {
                return _settings;
            }
            private set
            {
                _settings = value;
            }
        }

        private IWsl SetSettings(Settings settings)
        {
            _settings = settings;

            return this;
        }

        public IWsl SetIpAddress(string ipAddress)
        {
            _settings.IpAddress = ipAddress;
            return this;
        }

        public IWsl SetPort(int port)
        {
            _settings.Ports.Append<string>(port.ToString());
            return this;
        }

        public IWsl SetPorts(IEnumerable<string> ports)
        {
            foreach (var port in ports) {
                _settings.Ports.Append(port.ToString());
            }

            return this;
        }
    }
}
