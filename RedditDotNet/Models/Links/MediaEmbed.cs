using Newtonsoft.Json;

namespace RedditDotNet.Models.Links
{
    public class MediaEmbed
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("scrolling")]
        public bool Scrolling { get; set; }

        [JsonProperty("media_domain_url")]
        public string MediaDomainUrl { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
