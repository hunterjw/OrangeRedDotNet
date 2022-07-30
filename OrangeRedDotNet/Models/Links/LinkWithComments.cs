using Newtonsoft.Json;
using OrangeRedDotNet.Json;
using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Models.Listings;

namespace OrangeRedDotNet.Models.Links
{
    /// <summary>
    /// A Link with Comments
    /// </summary>
    [JsonConverter(typeof(LinkWithCommentsConverter))]
    public class LinkWithComments
    {
        /// <summary>
        /// The Link that was used to request comments for
        /// </summary>
        public Listing<Link> Links { get; set; }

        /// <summary>
        /// Comments for the link
        /// </summary>
        public Listing<CommentBase> Comments { get; set; }
    }
}
