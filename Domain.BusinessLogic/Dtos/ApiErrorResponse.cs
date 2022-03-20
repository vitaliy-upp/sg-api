using Newtonsoft.Json;

namespace Domain.BusinessLogic.Models
{
    public class ApiErrorResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("error")]
        public string Error { get; private set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; private set; }


        public ApiErrorResponse(string message)
            : this(0, message)
        {}

        public ApiErrorResponse(int errorCode, string message)
        {
            Type = "api";
            Error = message;
            ErrorCode = errorCode;
        }
    }
}
