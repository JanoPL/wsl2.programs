using System.Diagnostics;
using WSL;

namespace Strategies
{
    public class RemovePortProxyInformation : IStrategies
    {
        private readonly IWsl _wsl;

        public RemovePortProxyInformation(IWsl wsl)
        {
            _wsl = wsl;
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
                    string line = proc.StandardOutput.ReadLine();

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
