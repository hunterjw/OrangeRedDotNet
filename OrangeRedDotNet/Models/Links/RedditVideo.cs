using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Links
{
    public class RedditVideo
    {
        [JsonProperty("bitrate_kbps")]
        public int BitrateKbps { get; set; }

        [JsonProperty("fallback_url")]
        public string FallbackUrl { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("scrubber_media_url")]
        public string ScrubberMediaUrl { get; set; }

        [JsonProperty("dash_url")]
        public string DashUrl { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("hls_url")]
        public string HlsUrl { get; set; }

        [JsonProperty("is_gif")]
        public bool IsGif { get; set; }

        [JsonProperty("transcoding_status")]
        public string TranscodingStatus { get; set; }
    }
}
