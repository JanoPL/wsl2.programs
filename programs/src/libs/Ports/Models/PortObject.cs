using Newtonsoft.Json;

namespace Ports.Models
{
    public class PortObject
    {
        [JsonProperty("state")]
        public string? State { get; set; } = null;

        [JsonProperty("recv-q")]
        public string? ReceivBuffer { get; set; } = null;

        [JsonProperty("send-q")]
        public string? SendBuffer { get; set; } = null;

        [JsonProperty("local")]
        public string? LocalAddress { get; set; } = null;

        [JsonProperty("address:port")]
        public string? AddressPort { get; set; } = null;

        [JsonProperty("peer")]
        public string? PeerAddress { get; set; } = null;

        [JsonProperty("address:portprocess")]
        public string? PeerPortProcess { get; set; } = null;
    }
}