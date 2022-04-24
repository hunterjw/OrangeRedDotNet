using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Models;
using RedditDotNet.Models.Subreddits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// Subreddit operations
    /// </summary>
    public class SubredditsController : RedditController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userAgent">User agent string</param>
        /// <param name="redditAuthentication">Reddit authentication to use</param>
        public SubredditsController(string userAgent, IRedditAuthentication redditAuthentication)
            : base(userAgent, redditAuthentication) { }

        /// <summary>
        /// Return information about the subreddit.
        /// </summary>
        /// <param name="subredditName">Name of the subreddit</param>
        /// <returns>Subreddit information</returns>
        /// <exception cref="RedditApiException"></exception>
        public async Task<Thing<Subreddit>> GetAbout(string subredditName)
        {
            return await Get<Thing<Subreddit>>($"/r/{subredditName}/about");
        }

        /// <summary>
        /// Get the rules for the current subreddit
        /// </summary>
        /// <param name="subredditName">Name of the subreddit</param>
        /// <returns>Subreddit rules</returns>
        /// <exception cref="RedditApiException"></exception>
        public async Task<RulesResponse> GetRules(string subredditName)
        {
            return await Get<RulesResponse>($"/r/{subredditName}/about/rules");
        }
    }
}
