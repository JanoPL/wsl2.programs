using Microsoft.Extensions.Logging;
using WSL;

namespace Strategies.Strategy
{
    public class CheckWslPorts : IStrategies
    {
        private ILogger _logger;
        private IWsl _wsl;

        public CheckWslPorts(ILogger logger, IWsl wsl)
        {
            _logger = logger;
            _wsl = wsl;
        }

        public void Execute()
        {
            if (_wsl.Settings.Ports.Count == 0) {
                _logger.LogWarning("The specified ports were not found, at least one port is required");
                return;
            }
        }
    }
}
