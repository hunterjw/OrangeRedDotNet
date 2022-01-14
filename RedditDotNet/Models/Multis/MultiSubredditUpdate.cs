using Newtonsoft.Json;

namespace RedditDotNet.Models.Multis
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
