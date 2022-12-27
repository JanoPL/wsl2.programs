using CommandLine.Text;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace Portproxy
{
    public class AppConfig
    {
        [Option('l', "List", Required = false, HelpText = "show portproxy information")]
        public bool List { get; set; }

        [Option('d', "Delete", Required = false, HelpText = "Delete all portproxy information")]
        public bool Delete { get; set; }

        [Option('c', "Create", Required = false, HelpText = "Create all portproxy information")]
        public bool Create { get; set; }

        [Option('p', "list-ports", Required = false, HelpText = "List all listens ports information")]
        public bool ListPorts { get; set; }

        [Option('a', "list-address", Required = false, HelpText = "List Ip Address information from WSL2")]
        public bool ListIpAddress { get; set; }
    }
}
