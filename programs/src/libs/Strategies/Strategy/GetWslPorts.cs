using System.Data;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using Ports;
using Ports.Models;

namespace Strategies.Strategy
{
    public class GetWslPorts : IStrategies
    {
        private readonly ILogger _logger;
        private readonly IPorts _ports;

        public GetWslPorts(ILogger logger, IPorts ports)
        {
            _logger = logger;
            _ports = ports;
        }

        public void Execute()
        {
            Process proc = new() {
                StartInfo = new ProcessStartInfo() {
                    FileName = "wsl.exe",
                    Arguments = "ss -tlpH | column -J --table-columns State,Recv-Q,Send-Q,Local,Address:Port,Peer,Address:PortProcess",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            var start = proc.Start();

            if (!start) {
                _logger.LogError("Can't retriev socket statistics information");
                return;
            }

            _logger.LogInformation("Listen Ports");

            using (StreamReader streamReader = proc.StandardOutput) {
                string output = streamReader.ReadToEnd();
                proc.WaitForExit();
                _ports.ParseAsLogger(output);
            }
        }
    }
}
