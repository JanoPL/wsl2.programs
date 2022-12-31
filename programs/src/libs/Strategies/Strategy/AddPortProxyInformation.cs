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

                bool start = proc.Start();

                if (!start) {
                    _logger.LogError("Can't add portproxy information with: ");
                    _logger.LogError("listen address: ", address);
                    _logger.LogError("listen port: ", Wsl.Settings.Ports);
                    _logger.LogError("connection address: ", Wsl.Settings.IpAddress);
                    _logger.LogError("connection port: ", port);
                    return;
                }

                while (!proc.StandardOutput.EndOfStream) {
                    string? line = proc.StandardOutput.ReadLine();

                    if (line != null) { 
                        _logger.LogInformation("{line}", line);
                    }
                }
            }
        }
    }
}
