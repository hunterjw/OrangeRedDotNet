using Newtonsoft.Json;
using OrangeRedDotNet.Json;

namespace OrangeRedDotNet.Models.Comments
{
    /// <summary>
    /// Base class for Comments and More
    /// </summary>
    [JsonConverter(typeof(CommentBaseDataConverter))]
    public class CommentBaseData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("depth")]
        public int Depth { get; set; }
    }
}
