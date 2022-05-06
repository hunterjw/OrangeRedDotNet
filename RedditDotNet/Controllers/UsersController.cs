using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using RedditDotNet.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// User operations for Reddit
    /// </summary>
    public class UsersController : RedditController
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
        public UsersController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
            : base(redditAuthentication, redditUserAgent) { }

        /// <summary>
        /// Get user data by IDs
        /// </summary>
        /// <param name="ids">Account fullnames</param>
        /// <returns>Dictionary of account fullnames to user data</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Dictionary<string, UserData>> GetUsersByIds(IEnumerable<string> ids)
        {
            Dictionary<string, string> parameters = new()
            {
                { "ids", string.Join(',', ids) }
            };
            return await Get<Dictionary<string, UserData>>($"/api/user_data_by_account_ids", parameters);
        }

        /// <summary>
        /// Check whether a username is available for registration.
        /// </summary>
        /// <param name="username">A valid, unused, username</param>
        /// <returns>Boolean value</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<bool> IsUsernameAvailable(string username)
        {
            Dictionary<string, string> parameters = new()
            {
                { "user", username }
            };
            return await Get<bool>($"/api/username_available", parameters);
        }

        #region Friend operations
        /// <summary>
        /// Stop being friends with a user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task RemoveFriend(string username)
        {
            await Delete($"/api/v1/me/friends/{username}");
        }

        /// <summary>
        /// Get information about a specific 'friend', such as notes.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>User object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<User> GetFriendDetails(string username)
        {
            return await Get<User>($"/api/v1/me/friends/{username}");
        }

        /// <summary>
        /// Create or update a 'friend' relationship. 
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="note">A string no longer than 300 characters</param>
        /// <returns>User object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<User> UpdateFriend(string username, string note = "")
        {
            Dictionary<string, string> content = new()
            {
                {
                    "json",
                    $"{{ \"name\": \"{username}\"" +
                        (!string.IsNullOrWhiteSpace(note) ? $", \"note\": \"{note}\"" : "") +
                        $" }}"
                }
            };
            return await Put<User>($"/api/v1/me/friends/{username}", content);
        }
        #endregion

        /// <summary>
        /// Return a list of trophies for the a given user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>TrophyList object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<TrophyList> GetTrophies(string username)
        {
            return await Get<TrophyList>($"/api/v1/user/{username}/trophies");
        }

        /// <summary>
        /// Return information about the user, including karma and gold status.
        /// </summary>
        /// <param name="username">The name of an existing user</param>
        /// <returns>Account object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Account> GetAbout(string username)
        {
            return await Get<Account>($"/user/{username}/about");
        }

        #region Profile Page Listings
        /// <summary>
        /// Get a listing from a users page
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="listingType">Listing type (overview, submitted, 
        /// comments, upvoted, downvoted, hidden, saved, gilded)</param>
        /// <param name="parameters">Listing parameters</param>
        /// <returns>Listing of Links and/or Comments</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        public async Task<Listing<ILinkOrComment>> GetListing(string username, 
            UserProfileListingType listingType, UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/{listingType.ToDescriptionString()}", parameters);
        }
        #endregion
    }
}
