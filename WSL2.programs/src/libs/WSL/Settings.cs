namespace WSL
{
    public class Settings
    {
        private string[] _ports;
        private string _ipAddress;

        public string IpAddress { get => _ipAddress; set => _ipAddress = value; }
        public string[] Ports { get => _ports; set => _ports = value; } 
    }
}