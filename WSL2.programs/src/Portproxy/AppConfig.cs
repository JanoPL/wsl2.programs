using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Portproxy
{
    public class AppConfig : IAppConfig
    {
        public string Setting { get; }
        private ILogger<AppConfig> logger;

        public AppConfig(ILogger<AppConfig> logger)
        {
            this.logger = logger;
            this.logger.LogDebug("AppConfig constructed");
        }
    }
}
