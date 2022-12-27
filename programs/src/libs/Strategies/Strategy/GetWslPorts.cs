using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace Strategies.Strategy
{
    public class GetWslPorts : IStrategies
    {
        private ILogger _logger;

        public GetWslPorts(ILogger logger) 
        {
            _logger = logger;
        }

        public void Execute()
        {
            //TODO: GET listen ports from wsl instance - to finishing

            DataTable dataTable= new DataTable();
            dataTable
                .Columns
                .AddRange(
                    new DataColumn[] {
                        new DataColumn("Protocol"),
                        new DataColumn("Local Address"),
                        new DataColumn("Foreign Address"),
                        new DataColumn("State"),
                        new DataColumn("PID"),
                    }
                );

            Process proc = new() {
                StartInfo = new ProcessStartInfo() {
                    FileName = "wsl.exe",
                    Arguments = "ss -tlp",
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


            using (StreamReader r = proc.StandardOutput) {
                string output = r.ReadToEnd();
                proc.WaitForExit();

                string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string line in lines) {
                    string[] elements = line.Split(' ');
                    if (elements.Length < 5)
                        continue;
                    if (elements.Contains("Proto"))
                        continue;

                    DataRow dr = dataTable.NewRow();

                    List<string> validElements = new List<string>();

                    //Weed out empty elements.
                    foreach (string element in elements) {
                        //skip blanks
                        if (element.Trim() == "")
                            continue;
                        validElements.Add(element);
                    }

                    foreach (string element in validElements) {

                        foreach (DataColumn dc in dataTable.Columns) {
                            // fill in the buckets. Note that UDP doesn't have a state
                            if (dr["Protocol"].ToString() == "UDP" && dc.ColumnName == "State")
                                continue;

                            if (dr[dc] == DBNull.Value) {
                                dr[dc] = element;
                                break;
                            }
                        }
                    }

                    dataTable.Rows.Add(dr);
                }
            }

            _logger.LogInformation("Listen Ports");
            foreach (DataRow dataRow in dataTable.Rows) {
                _logger.LogInformation($"{dataRow.ToString()}");
            }
        }
    }
}
