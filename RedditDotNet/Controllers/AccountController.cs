using RedditDotNet.Authentication;
using RedditDotNet.Models;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.DTO;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<List<SubredditKarmaBreakdown>> GetKarmaBreakdown()
		{
			var result = await Get<Thing<List<SubredditKarmaBreakdown>>>("/api/v1/me/karma");
			return result.Data;
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
		public async Task<List<Award>> GetTrophies()
		{
			var result = await Get<Thing<TrophyListData>>("/api/v1/me/trophies");
			return result.Data.Trophies.Select(_ => _.Data).ToList();
		}
		
		/// <summary>
		/// Get the list of friends for the current user
		/// </summary>
		/// <returns>List of friends</returns>
		public async Task<List<User>> GetFriends()
		{
			var result = await Get<Thing<UserListData>>("/api/v1/me/friends");
			return result.Data.Children;
		}

		/// <summary>
		/// Get list of blocked users for the current user
		/// </summary>
		/// <returns>List of blocked users</returns>
		public async Task<List<User>> GetBlocked()
		{
			var result = await Get<Thing<UserListData>>("/prefs/blocked");
			return result.Data.Children;
		}

		/// <summary>
		/// Get list of trusted users for the current user
		/// </summary>
		/// <returns>List of trusted users</returns>
		public async Task<List<User>> GetTrusted()
		{
			var result = await Get<Thing<UserListData>>("/prefs/trusted");
			return result.Data.Children;
		}
	}
}
