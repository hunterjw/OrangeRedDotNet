using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Models;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
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
        public async Task<Subreddit> GetAbout(string subredditName)
        {
            return await Get<Subreddit>($"/r/{subredditName}/about");
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

        /// <summary>
        /// Get my subreddits
        /// </summary>
        /// <param name="type">Type of subreddits to get (subscriber, contributor, moderator, streams)</param>
        /// <param name="listingParameters">Listing parameters</param>
        /// <returns>Listing of subreddits</returns>
        /// <exception cref="RedditApiException"></exception>
        public async Task<Listing<Subreddit>> GetMine(MySubredditsType type, ListingParameters listingParameters = null)
        {
            return await Get<Listing<Subreddit>>($"/subreddits/mine/{type.ToDescriptionString()}", listingParameters);
        }

        /// <summary>
        /// Get subreddits
        /// </summary>
        /// <param name="type">Type of subreddits to get (popular, new, gold, default)</param>
        /// <param name="listingParameters">Listing parameters</param>
        /// <returns>Listing of subreddits</returns>
        /// <exception cref="RedditApiException"></exception>
        public async Task<Listing<Subreddit>> Get(SubredditsType type, ListingParameters listingParameters = null)
        {
            return await Get<Listing<Subreddit>>($"/subreddits/{type.ToDescriptionString()}", listingParameters);
        }
    }
}
