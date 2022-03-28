using Newtonsoft.Json;

namespace RedditDotNet.Models.Links
{
    public class Media
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("oembed")]
        public Oembed Oembed { get; set; }
    }
}
