using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
