using OrangeRedDotNet.Authentication;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Multis;
using OrangeRedDotNet.Models.Parameters.Multis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Controllers
{
    /// <summary>
    /// Controller for interacting with MultiReddits
    /// </summary>
    public class MultiController : RedditController
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
        public MultiController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
            : base(redditAuthentication, redditUserAgent) { }

        #region General operations
        /// <summary>
        /// Copy a MultiReddit
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>The newly created MultiReddit</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> CopyMulti(CopyMultiParameters parameters)
        {
            return await Post<MultiReddit>("/api/multi/copy", parameters);
        }

        /// <summary>
        /// Get my MultiReddits
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>List of my MultiReddits</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<List<MultiReddit>> GetMine(MultiParameters parameters = null)
        {
            return await Get<List<MultiReddit>>("/api/multi/mine", parameters);
        }

        /// <summary>
        /// Get MultiReddits for a specific user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>List of MultiReddits</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<List<MultiReddit>> GetByUsername(string username, MultiParameters parameters = null)
        {
            return await Get<List<MultiReddit>>($"/api/multi/user/{username}", parameters);
        }
        #endregion

        #region Multi
        /// <summary>
        /// Delete a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <returns>Awaitable task</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task DeleteMulti(string multiPath)
        {
            await Delete($"/api/multi/{multiPath.Trim('/', '\\')}");
        }

        /// <summary>
        /// Get a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> GetMulti(string multiPath, MultiParameters parameters = null)
        {
            return await Get<MultiReddit>($"/api/multi/{multiPath.Trim('/', '\\')}", parameters);
        }

        /// <summary>
        /// Create a new MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Newly create MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> CreateMulti(string multiPath, UpdateMultiParameters parameters)
        {
            return await Post<MultiReddit>($"/api/multi/{multiPath.Trim('/', '\\')}", parameters);
        }

        /// <summary>
        /// Update a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Updated MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> UpdateMulti(string multiPath, UpdateMultiParameters parameters)
        {
            return await Put<MultiReddit>($"/api/multi/{multiPath.Trim('/', '\\')}", parameters);
        }
        #endregion

        #region Multi Description
        /// <summary>
        /// Get a MultiReddit description
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <returns>Description object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiDescription> GetDescription(string multiPath)
        {
            return await Get<MultiDescription>($"/api/multi/{multiPath.Trim('/', '\\')}/description");
        }

        /// <summary>
        /// Update a MultiReddit description
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Updated description object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiDescription> UpdateDescription(string multiPath, UpdateDescriptionParameters parameters)
        {
            return await Put<MultiDescription>($"/api/multi/{multiPath.Trim('/', '\\')}/description", parameters);
        }
        #endregion

        #region Multi Subreddit
        /// <summary>
        /// Delete a Subreddit from a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit Path</param>
        /// <param name="subredditName">Subreddit name</param>
        /// <returns>Awaitable task</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task DeleteSubreddit(string multiPath, string subredditName)
        {
            await Delete($"/api/multi/{multiPath.Trim('/', '\\')}/r/{subredditName}");
        }

        /// <summary>
        /// Get a Subreddit on a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="subredditName">Subreddit name</param>
        /// <returns>MultiSubreddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiSubreddit> GetSubreddit(string multiPath, string subredditName)
        {
            return await Get<MultiSubreddit>($"/api/multi/{multiPath.Trim('/', '\\')}/r/{subredditName}");
        }

        /// <summary>
        /// Add a Subreddit to a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>MultiSubreddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiSubreddit> AddSubreddit(string multiPath, AddSubredditParameters parameters)
        {
            return await Put<MultiSubreddit>($"/api/multi/{multiPath.Trim('/', '\\')}/r/{parameters.SubredditName}", parameters);
        }
        #endregion
    }
}
