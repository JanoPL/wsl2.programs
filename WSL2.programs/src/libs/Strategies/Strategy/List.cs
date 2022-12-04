using System.Diagnostics;

namespace Strategies
{
    public class List : IStrategies
    {
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
