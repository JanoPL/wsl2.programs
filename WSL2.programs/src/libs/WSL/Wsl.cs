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

        public Wsl()
        {
            if (Settings == null) {
                _settings = new Settings();
            } else {
                _settings = Settings;
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

        public IWsl AddPort(int port)
        {
            _settings.Ports.Add(port.ToString());

            return this;
        }

        public IWsl AddPort(string port)
        {
            _settings.Ports.Add(port);
            return this;
        }

        public IWsl SetPorts(IList<string> ports)
        {
            _settings.Ports = ports;

            return this;
        }
    }
}
