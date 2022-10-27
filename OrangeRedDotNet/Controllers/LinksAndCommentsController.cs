using OrangeRedDotNet.Authentication;
using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.LinksAndComments;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;

namespace OrangeRedDotNet.Controllers
{
    /// <summary>
    /// Link and Comment operations
    /// </summary>
    public class LinksAndCommentsController : RedditController
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
        public LinksAndCommentsController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
            : base(redditAuthentication, redditUserAgent) { }

        /// <summary>
        /// Cast a vote on a thing.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Vote(VoteParameters parameters)
        {
            await Post("/api/vote", parameters);
        }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// </summary>
        /// <param name="parameters">MoreChildren parameters</param>
        /// <returns>List of comment/more objects</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<List<CommentBase>> GetMoreChildren(MoreChildrenParameters parameters)
        {
            var response = await Get<MoreChildrenResponse>("/api/morechildren", parameters);
            return response.Json.Data.Things;
        }

        /// <summary>
        /// Save a link or comment.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Save(ThingParameters parameters)
        {
            await Post("/api/save", parameters);
        }

        /// <summary>
        /// Unsave a link or comment.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Unsave(ThingParameters parameters)
        {
            await Post("/api/unsave", parameters);
        }

        /// <summary>
        /// Hide a link.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Hide(ThingParameters parameters)
        {
            await Post("/api/hide", parameters);
        }

        /// <summary>
        /// Unhide a link.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task Unhide(ThingParameters parameters)
        {
            await Post("/api/unhide", parameters);
        }

        /// <summary>
        /// Submit a link to a subreddit.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Submit response object</returns>
        public async Task<SubmitResponse> Submit(SubmitParameters parameters)
        {
            return await Post<SubmitResponse>("/api/submit", parameters);
        }

        /// <summary>
        /// Submit a new comment or reply to a message.
        /// </summary>
        /// <returns>Comment response object</returns>
        public async Task<CommentResponse> Comment(CommentParameters parameters)
        {
            return await Post<CommentResponse>("/api/comment", parameters);
        }
    }
}
