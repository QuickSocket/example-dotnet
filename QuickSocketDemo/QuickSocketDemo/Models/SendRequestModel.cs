using Newtonsoft.Json;

namespace QuickSocketDemo.Models
{
    public class SendRequestModel
    {
        [JsonProperty("connectionId")]
        public string ConnectionId { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
