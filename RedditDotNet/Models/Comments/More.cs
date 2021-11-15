using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Comments
{
    /// <summary>
    /// Summary of more comments
    /// </summary>
    public class More : CommentBase
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("children")]
        public List<string> Children { get; set; }
    }
}
