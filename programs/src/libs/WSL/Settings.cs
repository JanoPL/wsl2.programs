namespace WSL
{
    public class Settings
    {
        public virtual string? IpAddress { get; set; }
        public virtual IList<string> Ports { get; set; }

        public Settings()
        {
            IpAddress = string.Empty;
            Ports = new List<string>();
        }
    }
}