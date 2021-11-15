﻿using RedditDotNet.Authentication;
using RedditDotNet.Interfaces;
using RedditDotNet.Models;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
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
		/// Get the best of Reddit
		/// </summary>
		/// <param name="parameters">Listing parameter object</param>
		/// <returns>Listing of links (posts)</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetBest(ListingParameters parameters = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/best", parameters);
		}

		/// <summary>
		/// Get links by ID(s)
		/// </summary>
		/// <param name="fullNames">Full names of links to get</param>
		/// <returns>Listing of links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetByIds(IEnumerable<string> fullNames)
		{
			return await Get<Thing<Listing<Thing<Link>>>>($"/by_id/{string.Join(',', fullNames)}");
		}

		/// <summary>
		/// Get comments for a link
		/// </summary>
		/// <param name="articleId">Link ID</param>
		/// <param name="parameters">Comment listing parameters</param>
		/// <param name="subreddit">Subreddit for the link</param>
		/// <returns>Link with assosiated comments</returns>
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
		public async Task<Thing<Listing<Thing<Link>>>> GetHot(LocationListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/hot", parameters, subreddit);
		}

		/// <summary>
		/// Get the newest Links
		/// </summary>
		/// <param name="parameters">Listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetNew(ListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/new", parameters, subreddit);
		}

		/// <summary>
		/// Get a random set of Links
		/// </summary>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetRandom(string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/random", subreddit: subreddit);
		}

		/// <summary>
		/// Get rising Links
		/// </summary>
		/// <param name="parameters">Listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetRising(ListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/rising", parameters, subreddit);
		}

		/// <summary>
		/// Get the top Links
		/// </summary>
		/// <param name="parameters">Sort listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetTop(SortListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/top", parameters, subreddit);
		}

		/// <summary>
		/// Get the most controversial Links
		/// </summary>
		/// <param name="parameters">Sort listing parameters</param>
		/// <param name="subreddit">Subreddit to get Links for</param>
		/// <returns>Listing of Links</returns>
		public async Task<Thing<Listing<Thing<Link>>>> GetControversial(SortListingParameters parameters = null, string subreddit = null)
		{
			return await GetListingBySubreddit<Thing<Listing<Thing<Link>>>>("/controversial", parameters, subreddit);
		}

		/// <summary>
		/// Generic method to get Listings (optionally by Subreddit)
		/// </summary>
		/// <typeparam name="T">Type to deserialize json HTTP response content to</typeparam>
		/// <param name="relativeUrl">Relative URL for the request</param>
		/// <param name="parameters">Parameters for the request</param>
		/// <param name="subreddit">Subreddit to get content from</param>
		/// <returns>Deserialized json object</returns>
		private async Task<T> GetListingBySubreddit<T>(string relativeUrl, IQueryParameters parameters = null, string subreddit = null)
		{
			string url = string.Empty;
			if (!string.IsNullOrWhiteSpace(subreddit))
			{
				url += $"/r/{subreddit}";
			}
			url += relativeUrl;
			IDictionary<string, string> dict = null;
			if (parameters != null)
			{
				parameters.Validate();
				dict = parameters.ToQueryParameters();
			}
			return await Get<T>(url, dict);
		}
	}
}