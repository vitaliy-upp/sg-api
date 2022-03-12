using Newtonsoft.Json;

namespace MiroWhiteBoard.Models
{
    public class CreateBoardRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sharingPolicy")]
        public SharingPolicyRequest SharingPolicy { get; set; }
    }
}
