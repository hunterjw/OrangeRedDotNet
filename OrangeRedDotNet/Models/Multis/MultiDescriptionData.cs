using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Multis
{
    /// <summary>
    /// MultiReddit description data
    /// </summary>
    public class MultiDescriptionData
    {
        [JsonProperty("body_html")]
        public string BodyHtml { get; set; }

        [JsonProperty("body_md")]
        public string BodyMd { get; set; }
    }
}
