using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WSL;

namespace Strategies
{
    public class AddPortProxyInformation : IStrategies
    {
        private IWsl Wsl { get; set; }

        private readonly ILogger _logger;

        public AddPortProxyInformation(IWsl wsl, ILogger logger)
        {
            Wsl = wsl;
            _logger = logger;
        }
        
        public void Execute()
        {
            string address = "0.0.0.0";

            foreach (var port in Wsl.Settings.Ports) {
                var proc = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = "netsh.exe",
                        Arguments = $"interface portproxy add v4tov4 netsh interface portproxy add v4tov4 listenaddress={address} listenport={Wsl.Settings.Ports} connectaddress={Wsl.Settings.IpAddress} connectport={port}",
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
