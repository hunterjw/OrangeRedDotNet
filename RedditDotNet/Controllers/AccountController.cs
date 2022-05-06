﻿using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// Account operations for Reddit
    /// </summary>
    public class AccountController : RedditController
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
		public AccountController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null) 
			: base(redditAuthentication, redditUserAgent) { }

		/// <summary>
		/// Get the identity of the current user
		/// </summary>
		/// <returns>User Identity</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<AccountData> GetIdentity()
		{
			return await Get<AccountData>("/api/v1/me");
		}

		/// <summary>
		/// Get a breakdown of karma by subreddit for the current user
		/// </summary>
		/// <returns>Karma breakdown</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<KarmaBreakdown> GetKarmaBreakdown()
		{
			return await Get<KarmaBreakdown>("/api/v1/me/karma");
		}

		/// <summary>
		/// Get the user preferences for the current user
		/// </summary>
		/// <returns>User preferences</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<Preferences> GetPreferences()
		{
			return await Get<Preferences>("/api/v1/me/prefs");
		}

		/// <summary>
		/// Set the preferences for the current user
		/// </summary>
		/// <param name="preferences">User preferences</param>
		/// <returns>Updated user preferences</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<Preferences> SetPreferences(Preferences preferences)
        {
			return await Patch<Preferences>("/api/v1/me/prefs", 
				new Dictionary<string, string> { { "json", preferences.ToJson() } });
        }

		/// <summary>
		/// Get awards for the current user
		/// </summary>
		/// <returns>List of awards</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<TrophyList> GetTrophies()
		{
			return await Get<TrophyList>("/api/v1/me/trophies");
		}

		/// <summary>
		/// Get the list of friends for the current user
		/// </summary>
		/// <returns>List of friends</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<UserList> GetFriends()
		{
			return await Get<UserList>("/api/v1/me/friends");
 		}

		/// <summary>
		/// Get list of blocked users for the current user
		/// </summary>
		/// <returns>List of blocked users</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<UserList> GetBlocked()
		{
			return await Get<UserList>("/prefs/blocked");
		}

		/// <summary>
		/// Get list of trusted users for the current user
		/// </summary>
		/// <returns>List of trusted users</returns>
		/// <exception cref="RedditApiException"></exception>
		public async Task<UserList> GetTrusted()
		{
			return await Get<UserList>("/prefs/trusted");
		}
	}
}
