using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Comments
{
    public class CommentResponseData
    {
        [JsonProperty("things")]
        public List<CommentBase> Comments { get; set; }
    }
}
