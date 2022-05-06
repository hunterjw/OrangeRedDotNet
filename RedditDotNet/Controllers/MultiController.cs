using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Multis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
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
        /// <param name="from">MultiReddit path to copy from</param>
        /// <param name="to">MultiReddit path to copy to</param>
        /// <param name="displayName">Display name</param>
        /// <param name="descriptionMd">Description (markdown)</param>
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>The newly created MultiReddit</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> CopyMulti(string from, string to, string displayName = "", string descriptionMd = "", bool? expandSubreddits = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "from", from },
                { "to", to }
            };
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                parameters.Add("display_name", displayName);
            }
            if (!string.IsNullOrWhiteSpace(descriptionMd))
            {
                parameters.Add("description_md", descriptionMd);
            }
            if (expandSubreddits.HasValue && expandSubreddits.Value)
            {
                parameters.Add("expand_srs", expandSubreddits.Value.ToString());
            }
            return await Post<MultiReddit>("/api/multi/copy", parameters);
        }

        /// <summary>
        /// Get my MultiReddits
        /// </summary>
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>List of my MultiReddits</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<List<MultiReddit>> GetMine(bool? expandSubreddits = null)
        {
            Dictionary<string, string> parameters = new();
            if (expandSubreddits != null)
            {
                parameters.Add("expand_srs", expandSubreddits.ToString());
            }
            return await Get<List<MultiReddit>>("/api/multi/mine", parameters);
        }

        /// <summary>
        /// Get MultiReddits for a specific user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>List of MultiReddits</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<List<MultiReddit>> GetByUsername(string username, bool? expandSubreddits = null)
        {
            Dictionary<string, string> parameters = new();
            if (expandSubreddits != null)
            {
                parameters.Add("expand_srs", expandSubreddits.ToString());
            }
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
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> GetMulti(string multiPath, bool? expandSubreddits = null)
        {
            Dictionary<string, string> parameters = new();
            if (expandSubreddits != null)
            {
                parameters.Add("expand_srs", expandSubreddits.ToString());
            }
            return await Get<MultiReddit>($"/api/multi/{multiPath.Trim('/', '\\')}", parameters);
        }

        /// <summary>
        /// Create a new MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="model">New MultiReddit model</param>
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>Newly create MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> CreateMulti(string multiPath, MultiRedditUpdate model, bool? expandSubreddits = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "model", model.ToJson() }
            };
            if (expandSubreddits != null)
            {
                parameters.Add("expand_srs", expandSubreddits.ToString());
            }
            return await Post<MultiReddit>($"/api/multi/{multiPath.Trim('/', '\\')}", parameters);
        }

        /// <summary>
        /// Update a MultiReddit
        /// </summary>
        /// <param name="multiPath">MultiReddit path</param>
        /// <param name="model">New MultiReddit model</param>
        /// <param name="expandSubreddits">True to expand subreddits</param>
        /// <returns>Updated MultiReddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiReddit> UpdateMulti(string multiPath, MultiRedditUpdate model, bool? expandSubreddits = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "model", model.ToJson() }
            };
            if (expandSubreddits != null)
            {
                parameters.Add("expand_srs", expandSubreddits.ToString());
            }
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
        /// <param name="descriptionMd">Description (markdown)</param>
        /// <returns>Updated description object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiDescription> UpdateDescription(string multiPath, string descriptionMd)
        {
            var content = new Dictionary<string, string>
            {
                { "model", $"{{ \"body_md\": \"{descriptionMd}\" }}" }
            };
            return await Put<MultiDescription>($"/api/multi/{multiPath.Trim('/', '\\')}/description", content);
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
        /// <param name="subredditName">Subreddit name</param>
        /// <returns>MultiSubreddit object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<MultiSubreddit> AddSubreddit(string multiPath, string subredditName)
        {
            var content = new Dictionary<string, string>
            {
                { "model", $"{{ \"name\": \"{subredditName}\" }}" }
            };
            return await Put<MultiSubreddit>($"/api/multi/{multiPath.Trim('/', '\\')}/r/{subredditName}", content);
        }
        #endregion
    }
}
