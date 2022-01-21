using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using RedditDotNet.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
    /// <summary>
    /// User operations for Reddit
    /// </summary>
    public class UsersController : RedditController
    {
        public UsersController(string userAgent, IRedditAuthentication redditAuthentication)
            : base(userAgent, redditAuthentication) { }

        /// <summary>
        /// Get user data by IDs
        /// </summary>
        /// <param name="ids">Account fullnames</param>
        /// <returns>Dictionary of account fullnames to user data</returns>
        /// <exception cref="RedditApiException"></exception>
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
        public async Task<bool> IsUsernameAvailable(string username)
        {
            Dictionary<string, string> parameters = new()
            {
                { "user", username }
            };
            return await Get<bool>($"/api/username_available", parameters);
        }

        /// <summary>
        /// Get information about a specific 'friend', such as notes.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>User object</returns>
        /// <exception cref="RedditApiException"></exception>
        public async Task<User> GetFriendDetails(string username)
        {
            return await Get<User>($"/api/v1/me/friends/{username}");
        }

        /// <summary>
        /// Return a list of trophies for the a given user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>TrophyList object</returns>
        /// <exception cref="RedditApiException"></exception>
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
        public async Task<Account> GetAbout(string username)
        {
            return await Get<Account>($"/user/{username}/about");
        }

        #region Profile Page Listings
        /// <summary>
        /// Get an overview listing of user submissions
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetOverview(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/overview", parameters);
        }

        /// <summary>
        /// Get submitted links for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<Link>> GetSubmitted(string username, 
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<Link>>($"/user/{username}/submitted", parameters);
        }

        /// <summary>
        /// Get submitted comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<CommentBase>> GetComments(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<CommentBase>>($"/user/{username}/comments", parameters);
        }

        /// <summary>
        /// Get upvoted Links/Comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetUpvoted(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/upvoted", parameters);
        }

        /// <summary>
        /// Get downvoted Links/Comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetDownvoted(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/downvoted", parameters);
        }

        /// <summary>
        /// Get hidden Links/Comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetHidden(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/hidden", parameters);
        }

        /// <summary>
        /// Get saved Links/Comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetSaved(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/saved", parameters);
        }

        /// <summary>
        /// Get gilded Links/Comments for a user
        /// </summary>
        /// <param name="username">Reddit username</param>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>Listing of Links/Comments</returns>
        /// <exception cref="RedditApiException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Listing<ILinkOrComment>> GetGilded(string username,
            UsersListingParameters parameters = null)
        {
            return await Get<Listing<ILinkOrComment>>($"/user/{username}/gilded", parameters);
        }
        #endregion
    }
}
