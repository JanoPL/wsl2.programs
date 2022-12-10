using Microsoft.Extensions.Logging;

namespace Portproxy
{
    internal class Application
    {
        readonly IAppConfig config;
        public Application(IAppConfig config, ILogger<Application> logger)
        {
            this.config = config;
            logger.Log(LogLevel.Debug, "Application constructed");
        }

        public void Run()
        {

        }
    }
}
