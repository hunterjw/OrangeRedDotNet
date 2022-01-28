using Microsoft.AspNetCore.Components;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
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
        /// Results for the user link/comment overview
        /// </summary>
        protected Listing<ILinkOrComment> Overview { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrWhiteSpace(ListingType))
            {
                ListingType = "overview";
            }
            Overview = null;

            if (Account == null)
            {
                Account = await Reddit.Users.GetAbout(UserName);
            }
            if (Trophies == null)
            {
                Trophies = await Reddit.Users.GetTrophies(Account.Data.Name);
            }
            Overview = await Reddit.Users.GetOverview(Account.Data.Name, BuildUsersListingParameters());
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
        protected UserPageListingTypeEnum GetListingType()
        {
            return ListingType switch
            {
                "overview" => UserPageListingTypeEnum.Overview,
                _ => UserPageListingTypeEnum.Overview
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
    }
}
