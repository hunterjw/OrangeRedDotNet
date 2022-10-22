using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Subreddits
{
    public class SearchSubredditsResponse
    {
        [JsonProperty("subreddits")]
        public List<PartialSubreddit> Subreddits { get; set; }
    }
}
