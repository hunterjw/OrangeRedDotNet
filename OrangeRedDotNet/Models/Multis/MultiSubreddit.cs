using Newtonsoft.Json;
using OrangeRedDotNet.Models.Subreddits;

namespace OrangeRedDotNet.Models.Multis
{
    /// <summary>
    /// Subreddit on a MultiReddit
    /// </summary>
    public class MultiSubreddit
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public SubredditData Detail { get; set; }
    }
}
