using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.LinksAndComments
{
    internal class MoreChildrenResponse
    {
        [JsonProperty("json")]
        public Thing<MoreChildrenResponseData> Json { get; set; }
    }
}
