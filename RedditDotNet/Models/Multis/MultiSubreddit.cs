using Newtonsoft.Json;
using RedditDotNet.Models.Subreddits;

namespace RedditDotNet.Models.Multis
{
    /// <summary>
    /// Subreddit on a MultiReddit
    /// </summary>
    public class MultiSubreddit
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public SubredditDetail Detail { get; set; }
    }
}
