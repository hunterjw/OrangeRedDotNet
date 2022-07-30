using Newtonsoft.Json;

namespace OrangeRedDotNet.Models
{
    public class FlairRichtext
    {
        [JsonProperty("e")]
        public string Type { get; set; }
        [JsonProperty("t")]
        public string Text { get; set; }
        [JsonProperty("a")]
        public string Tag { get; set; }
        [JsonProperty("u")]
        public string Url { get; set; }
    }
}
