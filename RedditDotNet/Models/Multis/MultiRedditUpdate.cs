using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Multis
{
    /// <summary>
    /// MultiReddit update model
    /// </summary>
    public class MultiRedditUpdate
    {
        [JsonProperty("description_md")]
        public string DescriptionMd { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        // TODO Figure out how to pass this to the Put request successfully
        //[JsonProperty("icon_img")]
        //public string IconImage { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("subreddits")]
        public List<MultiSubredditUpdate> Subreddits { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }
}
