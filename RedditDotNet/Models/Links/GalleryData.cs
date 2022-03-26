using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Links
{
    public class GalleryData
    {
        [JsonProperty("items")]
        public List<GalleryItem> Items { get; set; }
    }
}
