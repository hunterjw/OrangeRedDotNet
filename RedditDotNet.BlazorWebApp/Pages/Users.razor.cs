using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System.Net.Http;
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
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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
        /// <summary>
        /// Sort of the things returned
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Sort { get; set; }
        /// <summary>
        /// Timescale of the things returned
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "t")]
        public string Timescale { get; set; }
        #endregion

        /// <summary>
        /// Account 
        /// </summary>
        protected Account Account { get; set; }
        /// <summary>
        /// Trophies for the current account
        /// </summary>
        protected TrophyList Trophies { get; set; }
        /// <summary>
        /// Results for the user link/comment listings
        /// </summary>
        protected Listing<ILinkOrComment> LinksOrComments { get; set; }
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

            Reddit redditClient = RedditService.GetClient();

            if (string.IsNullOrWhiteSpace(ListingType))
            {
                ListingType = "overview";
            }
            if (string.IsNullOrWhiteSpace(Sort))
            {
                Sort = "new";
            }
            if (string.IsNullOrWhiteSpace(Timescale) && (Sort.Equals("top") || Sort.Equals("controversial")))
            {
                Timescale = "hour";
            }

            if (ProfileLoaded && !UserName.Equals(Account.Data.Name))
            {
                ProfileLoaded = false;
                IsSelf = false;
            }

            if (!ProfileLoaded)
            {
                try
                {
                    Account = await redditClient.Users.GetAbout(UserName);
                    Trophies = await redditClient.Users.GetTrophies(Account.Data.Name);

                    if (RedditService.Identity != null
                        && Account.Data.Name.Equals(RedditService.Identity.Name))
                    {
                        IsSelf = true;
                        KarmaBreakdown = await redditClient.Account.GetKarmaBreakdown();
                    }

                    ProfileLoaded = true;
                }
                catch (RedditApiException rex)
                {
                    ToastService.ShowError(rex.MakeErrorMessage("Error loading profile"));
                }
            }

            try
            {
                LinksOrComments = await redditClient.Users.GetListing(
                    UserName, 
                    ListingType.ToEnumFromDescriptionString<UserProfileListingType>(), 
                    BuildUsersListingParameters());
                ListingLoaded = true;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading content"));
            }
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
                Sort = string.IsNullOrWhiteSpace(Sort) ? null : Sort.ToEnumFromDescriptionString<UsersListingSort>(),
                Timescale = string.IsNullOrWhiteSpace(Timescale) ? null : Timescale.ToEnumFromDescriptionString<Timescale>(),
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

        /// <summary>
        /// OnChange event handler for the timescale select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task TimescaleSelect_OnChange(ChangeEventArgs args)
        {
            UsersListingParameters parameters = BuildUsersListingParameters();
            parameters.Timescale = ((string)args.Value).ToEnumFromDescriptionString<Timescale>();
            // reset the position of the of the listing
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }

        /// <summary>
        /// OnChange event handler for the sort select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task SortSelect_OnChange(ChangeEventArgs args)
        {
            UsersListingParameters parameters = BuildUsersListingParameters();
            parameters.Sort = ((string)args.Value).ToEnumFromDescriptionString<UsersListingSort>();
            // reset the position of the of the listing
            parameters.Timescale = null;
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }
    }
}
