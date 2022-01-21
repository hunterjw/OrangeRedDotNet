using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Get the best of Reddit
		/// </summary>
		/// <param name="parameters">Listing parameter object</param>
		/// <returns>Listing of links (posts)</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetBest(ListingParameters parameters = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/best", parameters);
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
		/// Get the hottest Links
		/// </summary>
		/// <param name="parameters">Location based listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetHot(LocationListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/hot", parameters, subreddit);
		}

		/// <summary>
		/// Get the newest Links
		/// </summary>
		/// <param name="parameters">Listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetNew(ListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/new", parameters, subreddit);
		}

		/// <summary>
		/// Get a random set of Links
		/// </summary>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetRandom(string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/random", subreddit: subreddit);
		}

		/// <summary>
		/// Get rising Links
		/// </summary>
		/// <param name="parameters">Listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetRising(ListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/rising", parameters, subreddit);
		}

		/// <summary>
		/// Get the top Links
		/// </summary>
		/// <param name="parameters">Sort listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetTop(SortListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/top", parameters, subreddit);
		}

		/// <summary>
		/// Get the most controversial Links
		/// </summary>
		/// <param name="parameters">Sort listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetControversial(SortListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Listing<Link>>("/controversial", parameters, subreddit);
		}

		#region MultiReddit
		/// <summary>
		/// Get the hottest Links for a MultiReddit
		/// </summary>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="parameters">Location based listing parameters</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetHot(string multiRedditPath, LocationListingParameters parameters = null)
		{
			return await GetListingForMultiReddit<Listing<Link>>(multiRedditPath, "hot", parameters);
		}

		/// <summary>
		/// Get the newest Links for a MultiReddit
		/// </summary>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="parameters">Listing parameters</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetNew(string multiRedditPath, ListingParameters parameters = null)
		{
			return await GetListingForMultiReddit<Listing<Link>>(multiRedditPath, "new", parameters);
		}

		/// <summary>
		/// Get rising Links for a MultiReddit
		/// </summary>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="parameters">Listing parameters</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetRising(string multiRedditPath, ListingParameters parameters = null)
		{
			return await GetListingForMultiReddit<Listing<Link>>(multiRedditPath, "rising", parameters);
		}

		/// <summary>
		/// Get the top Links for a MultiReddit
		/// </summary>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="parameters">Sort listing parameters</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetTop(string multiRedditPath, SortListingParameters parameters = null)
		{
			return await GetListingForMultiReddit<Listing<Link>>(multiRedditPath, "top", parameters);
		}

		/// <summary>
		/// Get the most controversial Links for a MultiReddit
		/// </summary>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="parameters">Sort listing parameters</param>
		/// <returns>Listing of Links</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Listing<Link>> GetControversial(string multiRedditPath, SortListingParameters parameters = null)
		{
			return await GetListingForMultiReddit<Listing<Link>>(multiRedditPath, "controversial", parameters);
		}
		#endregion

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

		/// <summary>
		/// Generic method to get Listings for a MultiReddit
		/// </summary>
		/// <typeparam name="T">Type to deserialize json HTTP response content to</typeparam>
		/// <param name="multiRedditPath">MultiReddit relative path</param>
		/// <param name="relativeUrl">Relative URL for the request</param>
		/// <param name="parameters">Parameters for the request</param>
		/// <returns>Deserialized json object</returns>
		/// <exception cref="RedditApiException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private async Task<T> GetListingForMultiReddit<T>(string multiRedditPath, string relativeUrl, IQueryParameters parameters = null)
		{
			string url = multiRedditPath;
			if (!url.EndsWith('/'))
            {
				url += '/';
            }
			url += relativeUrl + "/";
			return await Get<T>(url, parameters);
		}
	}
}
