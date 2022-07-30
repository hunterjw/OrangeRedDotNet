using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Links
{
    public class GalleryItem
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
