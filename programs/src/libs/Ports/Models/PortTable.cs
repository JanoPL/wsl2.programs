using Newtonsoft.Json;

namespace Ports.Models
{
    public class PortTable
    {
        [JsonProperty]
        public IList<PortObject>? Table { get; set; } = new List<PortObject>();
    }
}
