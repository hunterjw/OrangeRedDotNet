using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Links
{
    public class MediaMetadata
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("e")]
        public string MediaType { get; set; }

        [JsonProperty("m")]
        public string MimeType { get; set; }

        [JsonProperty("p")]
        public List<MediaMetadataImage> Previews { get; set; }

        [JsonProperty("s")]
        public MediaMetadataImage Source { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("o")]
        public List<MediaMetadataImage> Obscured { get; set; }
    }
}
