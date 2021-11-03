using RedditDotNet.Authentication;
using RedditDotNet.Models;
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
		/// <param name="userAgent">Reddit user agent string</param>
		/// <param name="redditAuthentication">Authentication to use</param>
		public AccountController(string userAgent, IRedditAuthentication redditAuthentication) : base(userAgent, redditAuthentication)
		{
		}

		/// <summary>
		/// Get the identity of the current user
		/// </summary>
		/// <returns>User Identity</returns>
		public async Task<Identity> GetIdentity()
		{
			return await Get<Identity>("/api/v1/me");
		}

		/// <summary>
		/// Get a breakdown of karma by subreddit for the current user
		/// </summary>
		/// <returns>Karma breakdown</returns>
		public async Task<Thing<List<SubredditKarmaBreakdown>>> GetKarmaBreakdown()
		{
			return await Get<Thing<List<SubredditKarmaBreakdown>>>("/api/v1/me/karma");
		}

		/// <summary>
		/// Get the user preferences for the current user
		/// </summary>
		/// <returns>User preferences</returns>
		public async Task<Preferences> GetPreferences()
		{
			return await Get<Preferences>("/api/v1/me/prefs");
		}

		/// <summary>
		/// Get awards for the current user
		/// </summary>
		/// <returns>List of awards</returns>
		public async Task<Thing<TrophyList>> GetTrophies()
		{
			return await Get<Thing<TrophyList>>("/api/v1/me/trophies");
		}
		
		/// <summary>
		/// Get the list of friends for the current user
		/// </summary>
		/// <returns>List of friends</returns>
		public async Task<Thing<UserList>> GetFriends()
		{
			return await Get<Thing<UserList>>("/api/v1/me/friends");
 		}

		/// <summary>
		/// Get list of blocked users for the current user
		/// </summary>
		/// <returns>List of blocked users</returns>
		public async Task<Thing<UserList>> GetBlocked()
		{
			return await Get<Thing<UserList>>("/prefs/blocked");
		}

		/// <summary>
		/// Get list of trusted users for the current user
		/// </summary>
		/// <returns>List of trusted users</returns>
		public async Task<Thing<UserList>> GetTrusted()
		{
			return await Get<Thing<UserList>>("/prefs/trusted");
		}
	}
}
