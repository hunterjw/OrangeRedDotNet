using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Multis
{
    /// <summary>
    /// MultiSubreddit update model
    /// </summary>
    public class MultiSubredditUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
