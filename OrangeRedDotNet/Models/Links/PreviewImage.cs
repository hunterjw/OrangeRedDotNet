using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Links
{
    /// <summary>
    /// Preview images for a Link
    /// </summary>
	public class PreviewImage
    {
        [JsonProperty("source")]
        public ImageDetail Source { get; set; }

        [JsonProperty("resolutions")]
        public List<ImageDetail> Resolutions { get; set; }

        // TODO
        [JsonProperty("variants")]
        public Dictionary<string, PreviewImage> Variants { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
