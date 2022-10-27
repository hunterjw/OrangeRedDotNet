using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Comments
{
    public class CommentResponse
    {
        [JsonProperty("json")]
        public CommentResponseContent Content { get; set; }
    }
}
