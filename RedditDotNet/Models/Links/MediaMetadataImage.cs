using Newtonsoft.Json;

namespace RedditDotNet.Models.Links
{
    public class MediaMetadataImage
    {
        [JsonProperty("y")]
        public string Height { get; set; }

        [JsonProperty("x")]
        public string Width { get; set; }

        [JsonProperty("u")]
        public string Url { get; set; }
    }
}
