using Microsoft.AspNetCore.Components;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{

    /// <summary>
    /// Page for displaying user profiles
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }

        /// <summary>
        /// Identity service
        /// </summary>
        [Inject]
        public IdentityService IdentityService { get; set; }

        #region Route Parameters
        /// <summary>
        /// Account username
        /// </summary>
        [Parameter]
        public string UserName { get; set; }
        /// <summary>
        /// The listing type to display
        /// </summary>
        [Parameter]
        public string ListingType { get; set; }
        #endregion

        #region Query Parameters
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string After { get; set; }
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Before { get; set; }
        /// <summary>
        /// Number of items already retrieved
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public int? Count { get; set; }
        /// <summary>
        /// Maximum number of things to return
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public int? Limit { get; set; }
        #endregion

        /// <summary>
        /// Account 
        /// </summary>
        protected Models.Account.Account Account { get; set; }

        /// <summary>
        /// Trophies for the current account
        /// </summary>
        protected TrophyList Trophies { get; set; }

        /// <summary>
        /// Results for the user link/comment listings
        /// </summary>
        protected Listing<ILinkOrComment> LinksOrComments { get; set; }

        /// <summary>
        /// Results for the comments
        /// </summary>
        protected Listing<CommentBase> Comments { get; set; }

        /// <summary>
        /// Results for the links
        /// </summary>
        protected Listing<Link> Links { get; set; }

        /// <summary>
        /// If the profile is loaded or not
        /// </summary>
        protected bool ProfileLoaded { get; set; } = false;

        /// <summary>
        /// If the listing is loaded or not
        /// </summary>
        protected bool ListingLoaded { get; set; } = false;

        /// <summary>
        /// If the current profile is the current user
        /// </summary>
        protected bool IsSelf { get; set; } = false;

        /// <summary>
        /// Karma breakdown for the current user (if the user is self)
        /// </summary>
        protected KarmaBreakdown KarmaBreakdown { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            ListingLoaded = false;
            LinksOrComments = null;
            Comments = null;
            Links = null;
            if (ProfileLoaded && !UserName.Equals(Account.Data.Name))
            {
                ProfileLoaded = false;
                IsSelf = false;
            }

            if (!ProfileLoaded)
            {
                Account = await Reddit.Users.GetAbout(UserName);
                Trophies = await Reddit.Users.GetTrophies(Account.Data.Name);

                AccountData identity = await IdentityService.GetIdentity();
                if (Account.Data.Name.Equals(identity.Name))
                {
                    IsSelf = true;
                    KarmaBreakdown = await Reddit.Account.GetKarmaBreakdown();
                }

                ProfileLoaded = true;
            }

            if (string.IsNullOrWhiteSpace(ListingType))
            {
                ListingType = "overview";
            }
            switch (GetListingType())
            {
                case UserProfileListingType.Overview:
                    LinksOrComments = await Reddit.Users.GetOverview(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Comments:
                    Comments = await Reddit.Users.GetComments(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Submitted:
                    Links = await Reddit.Users.GetSubmitted(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Gilded:
                    LinksOrComments = await Reddit.Users.GetGilded(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Upvoted:
                    LinksOrComments = await Reddit.Users.GetUpvoted(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Downvoted:
                    LinksOrComments = await Reddit.Users.GetDownvoted(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Hidden:
                    LinksOrComments = await Reddit.Users.GetHidden(Account.Data.Name, BuildUsersListingParameters());
                    break;
                case UserProfileListingType.Saved:
                    LinksOrComments = await Reddit.Users.GetSaved(Account.Data.Name, BuildUsersListingParameters());
                    break;
            }
            ListingLoaded = true;
        }

        /// <summary>
        /// Helper function to get the relative URL for the current page
        /// </summary>
        /// <returns></returns>
        protected string GetRelativeUrl()
        {
            return $"/user/{UserName}/{ListingType}";
        }

        /// <summary>
        /// Get the current type of this listing
        /// </summary>
        /// <returns>Listing type</returns>
        protected UserProfileListingType GetListingType()
        {
            return ListingType switch
            {
                "overview" => UserProfileListingType.Overview,
                "comments" => UserProfileListingType.Comments,
                "submitted" => UserProfileListingType.Submitted,
                "gilded" => UserProfileListingType.Gilded,
                "upvoted" => UserProfileListingType.Upvoted,
                "downvoted" => UserProfileListingType.Downvoted,
                "hidden" => UserProfileListingType.Hidden,
                "saved" => UserProfileListingType.Saved,
                _ => UserProfileListingType.Overview
            };
        }

        /// <summary>
        /// Helper function to build the parameters object for the current page
        /// </summary>
        /// <returns>Parameters object</returns>
        protected ListingParameters BuildParameters()
        {
            return BuildUsersListingParameters();
        }

        /// <summary>
        /// Build a UsersListingParameters object
        /// </summary>
        /// <returns>UsersListingParameters object</returns>
        protected UsersListingParameters BuildUsersListingParameters()
        {
            return new UsersListingParameters
            {
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25,
            };
        }

        /// <summary>
        /// Get the loading quip for the listings
        /// </summary>
        /// <returns>Quip</returns>
        protected string GetLoadingQuip()
        {
            return GetListingType() switch
            {
                UserProfileListingType.Overview => "Loading overview...",
                UserProfileListingType.Comments => "Loading comments...",
                UserProfileListingType.Submitted => "Loading links...",
                UserProfileListingType.Gilded => "Loading gilded...",
                UserProfileListingType.Upvoted => "Loading upvoted...",
                UserProfileListingType.Downvoted => "Loading downvoted...",
                UserProfileListingType.Hidden => "Loading hidden...",
                UserProfileListingType.Saved => "Loading saved...",
                _ => "Loading..."
            };
        }
    }
}
