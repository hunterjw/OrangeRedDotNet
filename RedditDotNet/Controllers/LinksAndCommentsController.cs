using RedditDotNet.Authentication;
using System.Collections.Generic;
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
    }
}
