using Newtonsoft.Json;
using OrangeRedDotNet.Models.Comments;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.LinksAndComments
{
    internal class MoreChildrenResponseData
    {
        [JsonProperty("things")]
        public List<CommentBase> Things { get; set; }
    }
}
