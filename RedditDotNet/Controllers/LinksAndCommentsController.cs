using RedditDotNet.Authentication;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.LinksAndComments;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// Link and Comment operations
    /// </summary>
    public class LinksAndCommentsController : RedditController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userAgent">User agent string</param>
        /// <param name="redditAuthentication">Reddit authentication to use</param>
        public LinksAndCommentsController(string userAgent, IRedditAuthentication redditAuthentication) 
            : base(userAgent, redditAuthentication) { }

        /// <summary>
        /// Cast a vote on a thing.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="dir">vote direction. one of (1, 0, -1)</param>
        /// <param name="rank">an integer greater than 1</param>
        /// <returns>Awaitable task</returns>
        public async Task Vote(string id, int dir, int rank)
        {
            Dictionary<string, string> parameters = new()
            {
                { "id", id },
                { "dir", $"{dir}" },
                { "rank", $"{rank}" }
            };
            await Post("/api/vote", parameters);
        }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// </summary>
        /// <param name="linkFullName">Fullname of the link whose comments are being fetched</param>
        /// <param name="children">List of comment ID36s that need to be fetched</param>
        /// <param name="sort">The sort on the comments returned</param>
        /// <param name="depth">Maximum depth of subtrees in the thread to get</param>
        /// <param name="limitChildren">Only return the children requested</param>
        /// <returns>List of comment/more objects</returns>
        public async Task<List<CommentBase>> GetMoreChildren(string linkFullName, IEnumerable<string> children,
            CommentSort? sort = null, int? depth = null, bool? limitChildren = false)
        {
            Dictionary<string, string> parameters = new()
            {
                { "api_type", "json" },
                { "children", string.Join(',', children) },
                { "link_id", linkFullName },
            };
            if (sort.HasValue)
            {
                parameters.Add("sort", sort.Value.ToDescriptionString());
            }
            if (depth.HasValue)
            {
                parameters.Add("depth", $"{depth}");
            }
            if (limitChildren ?? false)
            {
                parameters.Add("limit_children", $"{limitChildren}");
            }

            var response = await Get<MoreChildrenResponse>("/api/morechildren", parameters);

            return response.Json.Data.Things;
        }
    }
}
