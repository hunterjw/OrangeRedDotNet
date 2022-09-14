using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Subreddits;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Component to display duplicate links
    /// </summary>
    public partial class Duplicates
    {
        #region Injected services
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        #endregion

        #region Route parameters
        /// <summary>
        /// Subreddit name
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }
        /// <summary>
        /// Link/Article ID
        /// </summary>
        [Parameter]
        public string ArticleId { get;set; }
        #endregion

        #region Query parameters
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
        /// Duplicate links object
        /// </summary>
        protected DuplicateLinks DuplicateLinks { get; set; }
        /// <summary>
        /// Subreddit details
        /// </summary>
        protected Subreddit SubredditDetails { get; set; }
        /// <summary>
        /// Subreddit details loaded
        /// </summary>
        protected bool SubredditDetailsLoaded { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var redditClient = RedditService.GetClient();

                DuplicateLinks = null;

                if (SubredditDetailsLoaded && !SubredditDetails.Data.DisplayName.Equals(Subreddit))
                {
                    SubredditDetailsLoaded = false;
                }
                if (!SubredditDetailsLoaded && !Subreddit.IsSpecialSubreddit())
                {
                    SubredditDetails = await redditClient.Subreddits.GetAbout(Subreddit);
                    SubredditDetailsLoaded = true;
                }

                DuplicateLinks = await redditClient.Listings.GetDuplicates(
                    ArticleId,
                    BuildDuplicateListingParameters());

                var linkSubredditName = DuplicateLinks?.Originals?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
                if (!string.IsNullOrWhiteSpace(linkSubredditName))
                {
                    Subreddit = linkSubredditName;
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading duplicates"));
            }
        }

        /// <summary>
        /// Build a duplicate links object based on the current component arguments
        /// </summary>
        /// <returns>Duplicate links object</returns>
        protected DuplicateListingParameters BuildDuplicateListingParameters()
        {
            return new DuplicateListingParameters 
            {
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25,
            };
        }

        /// <summary>
        /// Build a listing parameters object based on the current component arguments
        /// </summary>
        /// <returns>Listing parameters object</returns>
        protected ListingParameters BuildParameters()
        {
            return BuildDuplicateListingParameters();
        }

        /// <summary>
        /// Get the current relative URL without parameters
        /// </summary>
        /// <returns>Relative URL</returns>
        protected string GetRelativeUrl()
        {
            return $"/r/{Subreddit}/duplicates/{ArticleId}";
        }
    }
}
