using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ports.Models;

namespace Ports
{
    public interface IPorts
    {
        public string? ParseAsString(string lines, bool newLine);
        public PortTable? ParseAsObject(string lines);
        public void ParseAsLogger(string lines);
    }
}
