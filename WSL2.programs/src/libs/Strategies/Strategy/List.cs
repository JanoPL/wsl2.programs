using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Strategies
{
    public class List : IStrategies
    {
        private readonly ILogger _logger;

        public List(ILogger logger) 
        {
            _logger = logger;
        }

        public void Execute()
        {
            var proc = new Process() {
                StartInfo = new ProcessStartInfo {
                    FileName = "netsh.exe",
                    Arguments = "interface portproxy show v4tov4",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            proc.Start();

            while (!proc.StandardOutput.EndOfStream) {
                string? line = proc.StandardOutput.ReadLine();

                if (line == null) {
                    proc.WaitForExit();
                    return;
                }

                if (string.IsNullOrEmpty(line)) {
                    _logger.LogInformation("There is no portproxy information");
                    return;
                }

                if (line != null) {
                    _logger.LogInformation("{line}", line);
                }
            }
        }
    }
}
