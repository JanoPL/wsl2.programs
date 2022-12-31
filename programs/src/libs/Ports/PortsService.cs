using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ports.Models;

namespace Ports
{
    public class PortsService : IPorts
    {
        private readonly ILogger<PortsService> _logger;
        public PortsService(ILogger<PortsService> logger)
        {
            _logger = logger;
        }

        private static StringBuilder GetStringBuilder(bool newLine, PortTable? ports)
        {
            StringBuilder sb = new();
            if (ports != null && ports.Table != null) {
                foreach (PortObject item in ports.Table) {
                    var stringItem = item.State + " " + item.ReceivBuffer + " " + item.SendBuffer + " " + item.LocalAddress + " " + item.AddressPort + " " + item.PeerAddress + " " + item.PeerPortProcess;

                    if (newLine) {
                        sb.AppendLine(stringItem);
                    } else {
                        sb.Append(stringItem);
                        sb.AppendLine();
                    }
                }
            }

            return sb;
        }

        private static T? GetDeserializeObject<T>(string lines) where T : class
        {
            lines = lines.Replace("\n", "").Replace("\r", "");

            T? ports = JsonConvert.DeserializeObject<T>(lines);

            return ports;
        }

        public string? ParseAsString(string lines, bool newLine)
        {
            PortTable? ports = PortsService.GetDeserializeObject<PortTable>(lines);

            StringBuilder sb = GetStringBuilder(newLine, ports);

            return sb.ToString();
        }


        public PortTable? ParseAsObject(string lines)
        {
            lines = lines.Replace("\n", "");

            PortTable? ports = JsonConvert.DeserializeObject<PortTable>(lines);

            return ports;
        }

        public void ParseAsLogger(string lines)
        {
            PortTable? ports = PortsService.GetDeserializeObject<PortTable>(lines);

            if (ports != null && ports.Table != null) {
                foreach (PortObject item in ports.Table) {
                    var stringItem = item.State + " " + item.ReceivBuffer + " " + item.SendBuffer + " " + item.LocalAddress + " " + item.AddressPort + " " + item.PeerAddress + " " + item.PeerPortProcess;

                    _logger.LogInformation("{stringItem}", stringItem);
                }
            }
        }
    }
}
