using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Comments
{
    public class CommentResponseContent
    {
        [JsonProperty("data")]
        public CommentResponseData Data { get; set; }

        [JsonProperty("errors")]
        public List<List<string>> Errors { get; set; }
    }
}
