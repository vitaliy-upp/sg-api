using Newtonsoft.Json;

namespace MiroWhiteBoard.Models
{
    public class SharingPolicyRequest
    {
        [JsonProperty("access")]
        public string Access { get; set; }
        [JsonProperty("teamAccess")]
        public string TeamAccess { get; set; }
    }
}
