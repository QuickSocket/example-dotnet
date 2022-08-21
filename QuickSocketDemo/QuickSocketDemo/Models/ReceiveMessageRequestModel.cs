using Newtonsoft.Json;

namespace QuickSocketDemo.Models
{
    public class ReceiveMessageRequestModel
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("connectionId")]
        public string ConnectionId { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
