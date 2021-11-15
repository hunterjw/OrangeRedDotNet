using Newtonsoft.Json;
using RedditDotNet.Json;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.Models.Links
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
        public Thing<Listing<Thing<Link>>> Links { get; set; }

        /// <summary>
        /// Comments for the link
        /// </summary>
        public Thing<Listing<Thing<CommentBase>>> Comments { get; set; }
    }
}
