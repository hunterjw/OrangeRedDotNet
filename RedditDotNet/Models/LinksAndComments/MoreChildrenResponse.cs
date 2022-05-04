using Newtonsoft.Json;

namespace RedditDotNet.Models.LinksAndComments
{
    internal class MoreChildrenResponse
    {
        [JsonProperty("json")]
        public Thing<MoreChildrenResponseData> Json { get; set; }
    }
}
