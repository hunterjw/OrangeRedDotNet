using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Links
{
    public class Media
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("oembed")]
        public Oembed Oembed { get; set; }

        [JsonProperty("reddit_video")]
        public RedditVideo RedditVideo { get; set; }
    }
}
