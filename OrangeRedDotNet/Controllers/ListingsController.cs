using OrangeRedDotNet.Authentication;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Interfaces;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Controllers
{
    /// <summary>
    /// Listings operations for Reddit
    /// </summary>
    public class ListingsController : RedditController
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
        public ListingsController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
            : base(redditAuthentication, redditUserAgent) { }

        /// <summary>
        /// Get links from the front page of Reddit
        /// </summary>
        /// <param name="listingType">Type of listing to get</param>
        /// <param name="parameters">Listing parameters</param>
        /// <returns>Link listing</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<Link>> GetLinks(FrontPageListingType listingType, ListingParameters parameters = null)
        {
            return await Get<Listing<Link>>($"/{listingType.ToDescriptionString()}", parameters);
        }

        /// <summary>
        /// Get links for a subreddit
        /// </summary>
        /// <param name="listingType">Listing type</param>
        /// <param name="subreddit">Subreddit name</param>
        /// <param name="parameters">Listing parameters</param>
        /// <returns>Link listing</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<Link>> GetLinksForSubreddit(LinkListingType listingType, string subreddit, ListingParameters parameters = null)
        {
            return await Get<Listing<Link>>($"/r/{subreddit}/{listingType.ToDescriptionString()}", parameters);
        }

        /// <summary>
        /// Get links from a multireddit
        /// </summary>
        /// <param name="listingType">Listing type</param>
        /// <param name="multiRedditPath">Multireddit path</param>
        /// <param name="parameters">Listing parameters</param>
        /// <returns>Link listing</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<Link>> GetLinksForMultireddit(LinkListingType listingType, string multiRedditPath, ListingParameters parameters = null)
        {
            string url = multiRedditPath;
            if (!url.EndsWith('/'))
            {
                url += '/';
            }
            return await Get<Listing<Link>>($"{url}{listingType.ToDescriptionString()}", parameters);
        }

        /// <summary>
        /// Get links by ID(s)
        /// </summary>
        /// <param name="fullNames">Full names of links to get</param>
        /// <returns>Listing of links</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<Link>> GetByIds(IEnumerable<string> fullNames)
        {
            return await Get<Listing<Link>>($"/by_id/{string.Join(',', fullNames)}");
        }

        /// <summary>
        /// Get comments for a link
        /// </summary>
        /// <param name="articleId">Link ID</param>
        /// <param name="parameters">Comment listing parameters</param>
        /// <param name="subreddit">Subreddit for the link</param>
        /// <returns>Link with assosiated comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<LinkWithComments> GetComments(string articleId, CommentListingParameters parameters = null, string subreddit = null)
        {
            return await GetListingBySubreddit<LinkWithComments>($"/comments/{articleId}", parameters, subreddit);
        }

        /// <summary>
        /// Get a listing of duplicate links for a given link
        /// </summary>
        /// <param name="articleId">Link ID</param>
        /// <param name="parameters">Duplicate listing parameters</param>
        /// <returns>Original link with the duplicate links</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<DuplicateLinks> GetDuplicates(string articleId, DuplicateListingParameters parameters = null)
        {
            return await GetListingBySubreddit<DuplicateLinks>($"/duplicates/{articleId}", parameters);
        }

        /// <summary>
        /// Generic method to get Listings (optionally by Subreddit)
        /// </summary>
        /// <typeparam name="T">Type to deserialize json HTTP response content to</typeparam>
        /// <param name="relativeUrl">Relative URL for the request</param>
        /// <param name="parameters">Parameters for the request</param>
        /// <param name="subreddit">Subreddit to get content from</param>
        /// <returns>Deserialized json object</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        private async Task<T> GetListingBySubreddit<T>(string relativeUrl, IQueryParameters parameters = null, string subreddit = null)
        {
            string url = string.Empty;
            if (!string.IsNullOrWhiteSpace(subreddit))
            {
                url += $"/r/{subreddit}";
            }
            url += relativeUrl;
            return await Get<T>(url, parameters);
        }
    }
}
