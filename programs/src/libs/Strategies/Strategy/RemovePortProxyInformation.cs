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
            foreach (string port in _wsl.Settings.Ports) {
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

                bool start = proc.Start();

                if (!start) {
                    _logger.LogError("Can't remove portproxy information, with: ");
                    _logger.LogError("listen address {listenAddress}", _wsl.Settings.IpAddress);
                    _logger.LogError("listen port {port}", port);
                    return;
                }

                while (!proc.StandardOutput.EndOfStream) {
                    string? line = proc.StandardOutput.ReadLine();

                    if (line == null) {
                        _logger.LogInformation("{line}", line);
                    }
                }
            }
        }
    }
}
