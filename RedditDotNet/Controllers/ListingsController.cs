using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// Listings operations for Reddit
    /// </summary>
    public class ListingsController : RedditController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userAgent">Reddit user agent string</param>
        /// <param name="redditAuthentication">Authentication to use</param>
        public ListingsController(string userAgent, IRedditAuthentication redditAuthentication) : base(userAgent, redditAuthentication)
        {
        }

        /// <summary>
        /// Get links from the front page of Reddit
        /// </summary>
        /// <param name="listingType">Type of listing to get</param>
        /// <param name="parameters">Listing parameters</param>
        /// <returns>Link listing</returns>
        /// <exception cref="RedditApiException"></exception>
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
