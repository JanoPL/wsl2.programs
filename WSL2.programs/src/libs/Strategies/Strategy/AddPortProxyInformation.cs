using System.Diagnostics;
using WSL;

namespace Strategies
{
    public class AddPortProxyInformation : IStrategies
    {
        private readonly IWsl _wsl;
        public AddPortProxyInformation(IWsl wsl)
        {
            _wsl = wsl;
        }
        public void Execute()
        {
            string address = "0.0.0.0";

            foreach (var port in _wsl.Settings.Ports) {
                var proc = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = "netsh.exe",
                        Arguments = $"interface portproxy add v4tov4 netsh interface portproxy add v4tov4 listenaddress={address} listenport={_wsl.Settings.Ports} connectaddress={_wsl.Settings.IpAddress} connectport={port}",
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
                        Console.WriteLine("There is no portproxy information");
                        return;
                    }

                    Console.WriteLine(line);
                }
            }
        }
    }
}
