using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WSL;

namespace Strategies
{
    public class CheckWslIPAddress : IStrategies
    {
        readonly private IWsl _wsl;
        readonly private ILogger<CheckWslIPAddress> _logger;
        public CheckWslIPAddress(ILogger<CheckWslIPAddress> logger, IWsl wsl)
        {
            _logger = logger;
            _wsl = wsl;
        }

        public void Execute()
        {
            Process proc = new Process {
                StartInfo = new ProcessStartInfo() {
                    FileName = "wsl.exe",
                    Arguments = "hostname -I",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            proc.Start();


            while (!proc.StandardOutput.EndOfStream) {
                string? ipAddress = proc.StandardOutput.ReadLine();

                if (string.IsNullOrEmpty(ipAddress)) {
                    _logger.LogError("WSL2 cannot be found.");
                    return;
                }

                _wsl.SetIpAddress(ipAddress);

                _logger.LogInformation($"Ip address: {ipAddress}");
            }
        }
    }
}
