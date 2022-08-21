using Newtonsoft.Json;

namespace QuickSocketDemo.Models
{
    public class AuthenticateConnectionRequestModel
    {
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }
    }
}
