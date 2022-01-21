using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Parameters;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Component to display duplicate links
    /// </summary>
    public partial class Duplicates
    {
        #region Injected services
        /// <summary>
        /// Reddit client
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }
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

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            DuplicateLinks = null;

            DuplicateLinks = await Reddit.Listings.GetDuplicates(
                ArticleId,
                BuildDuplicateListingParameters());

            var linkSubredditName = DuplicateLinks?.Originals?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
            if (!string.IsNullOrWhiteSpace(linkSubredditName))
            {
                Subreddit = linkSubredditName;
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
