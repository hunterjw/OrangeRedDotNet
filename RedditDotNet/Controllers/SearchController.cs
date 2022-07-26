using RedditDotNet.Authentication;
using RedditDotNet.Models.Parameters;
using RedditDotNet.Models.Search;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// Search controller
    /// </summary>
    public class SearchController : RedditController
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
        public SearchController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null) :
            base(redditAuthentication, redditUserAgent)
        { }

        /// <summary>
        /// Search Reddit
        /// </summary>
        /// <param name="parameters">Search parameters</param>
        /// <param name="subreddit">Subreddit name</param>
        /// <returns>Search results object</returns>
        public async Task<SearchResults> Search(SearchListingParameters parameters, string subreddit = null)
        {
            return await Get<SearchResults>(
                $"{(string.IsNullOrWhiteSpace(subreddit) ? "" : $"/r/{subreddit}")}/search", 
                parameters);
        }
    }
}
