using Newtonsoft.Json;
using RedditDotNet.Json;

namespace RedditDotNet.Interfaces
{
    /// <summary>
    /// Interface for Link or Comment objects
    /// </summary>
    [JsonConverter(typeof(LinkOrCommentConverter))]
    public interface ILinkOrComment { }
}
