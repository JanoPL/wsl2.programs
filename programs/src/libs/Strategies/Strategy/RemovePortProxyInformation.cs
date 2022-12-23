using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WSL;

namespace Strategies
{
    public class RemovePortProxyInformation : IStrategies
    {
        private readonly IWsl _wsl;
        private readonly ILogger _logger;

        public RemovePortProxyInformation(IWsl wsl, ILogger logger)
        {
            _wsl = wsl;
            _logger = logger;
        }

        public void Execute()
        {
            foreach (var port in _wsl.Settings.Ports) {
                var proc = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = "netsh.exe",
                        Arguments = $"interface portproxy delete v4tov4 listenaddress={_wsl.Settings.IpAddress} listenport={port}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    }
                };

                proc.Start();

                while (!proc.StandardOutput.EndOfStream) {
                    string? line = proc.StandardOutput.ReadLine();

                    if (string.IsNullOrEmpty(line)) {
                        _logger.LogInformation("There is no portproxy information");
                        return;
                    }

                    _logger.LogInformation("{line}", line);
                }
            }
        }
    }
}
