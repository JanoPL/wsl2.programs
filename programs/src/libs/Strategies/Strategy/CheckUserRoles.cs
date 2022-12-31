using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Strategies.Strategy
{
    public class CheckUserRoles : IStrategies
    {
        private readonly ILogger _logger;
        public CheckUserRoles(ILogger logger) 
        {
            _logger = logger;
        }

        public void Execute()
        {
            if (!IsUserAnAdmin()) {
                _logger.LogError("This command required Administrator permision, try run as Administrator");
                return;
            }
        }

        [DllImport("shell32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsUserAnAdmin();
    }
}
