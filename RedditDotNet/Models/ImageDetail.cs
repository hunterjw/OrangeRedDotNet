using Newtonsoft.Json;

namespace RedditDotNet.Models
{
    /// <summary>
    /// Details of an image
    /// </summary>
	public class ImageDetail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
