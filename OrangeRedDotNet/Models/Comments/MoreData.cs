using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Comments
{
    /// <summary>
    /// Summary of more comments
    /// </summary>
    public class MoreData : CommentBaseData
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("children")]
        public List<string> Children { get; set; }
    }
}
