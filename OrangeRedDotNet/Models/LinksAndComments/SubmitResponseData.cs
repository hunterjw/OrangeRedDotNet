using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.LinksAndComments
{
    public class SubmitResponseData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("drafts_count")]
        public int DraftsCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
