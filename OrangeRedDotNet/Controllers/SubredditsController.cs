using OrangeRedDotNet.Authentication;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Subreddits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Controllers
{
    /// <summary>
    /// Subreddit operations
    /// </summary>
    public class SubredditsController : RedditController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="redditAuthentication">Authentication to use</param>
        /// <param name="redditUserAgent">
        ///		Reddit user agent.
        ///		If the reddit client is being used within a web application hosted in a browser 
        ///		(i.e. Blazor Webassembly), do not provide a user agent as the browsers user agent
        ///		will be used instead.
        ///	</param>
        public SubredditsController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
            : base(redditAuthentication, redditUserAgent) { }

        /// <summary>
        /// Return information about the subreddit.
        /// </summary>
        /// <param name="subredditName">Name of the subreddit</param>
        /// <returns>Subreddit information</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
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
		/// <exception cref="RedditAuthenticationException"></exception>
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
		/// <exception cref="RedditAuthenticationException"></exception>
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
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<Subreddit>> Get(SubredditsType type, ListingParameters listingParameters = null)
        {
            return await Get<Listing<Subreddit>>($"/subreddits/{type.ToDescriptionString()}", listingParameters);
        }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit
        /// </summary>
        /// <param name="subredditName">Subreddit name</param>
        /// <param name="action">Subscribe action</param>
        /// <param name="skipInitialDefaults">
        ///     Set to True to prevent automatically subscribing the user to the current set of 
        ///     defaults when they take their first subscription action. Attempting to set it 
        ///     for an unsubscribe action will result in an error.
        /// </param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Subscribe(string subredditName, SubscribeAction action, bool skipInitialDefaults = false)
        {
            Dictionary<string, string> parameters = new()
            {
                { "sr_name", subredditName },
                { "action", action.ToDescriptionString() },
                { "skip_initial_defaults", $"{skipInitialDefaults}" },
            };
            await Post("/api/subscribe", parameters);
        }
    }
}
