using Newtonsoft.Json;
using OrangeRedDotNet.Json;

namespace OrangeRedDotNet.Interfaces
{
    /// <summary>
    /// Interface for Link or Comment objects
    /// </summary>
    [JsonConverter(typeof(LinkOrCommentConverter))]
    public interface ILinkOrComment { }
}
