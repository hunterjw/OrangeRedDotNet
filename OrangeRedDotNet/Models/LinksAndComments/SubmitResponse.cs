using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.LinksAndComments
{
    public class SubmitResponse
    {
        [JsonProperty("json")]
        public SubmitResponseContent Content { get; set; }
    }
}
