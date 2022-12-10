using Microsoft.Extensions.Logging;

namespace Portproxy
{
    public class AppConfig : IAppConfig
    {
        public string? Settings { get; }
        private ILogger<AppConfig> logger;

        public AppConfig(ILogger<AppConfig> logger)
        {
            this.logger = logger;
            this.logger.LogDebug("AppConfig constructed");
        }
    }
}
