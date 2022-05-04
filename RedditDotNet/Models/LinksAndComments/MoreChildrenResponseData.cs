using Newtonsoft.Json;
using RedditDotNet.Models.Comments;
using System.Collections.Generic;

namespace RedditDotNet.Models.LinksAndComments
{
    internal class MoreChildrenResponseData
    {
        [JsonProperty("things")]
        public List<CommentBase> Things { get; set; }
    }
}
