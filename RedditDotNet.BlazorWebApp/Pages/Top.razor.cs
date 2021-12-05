using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    public partial class Top
    {
        #region Injected Services
        /// <summary>
        /// Reddit client
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }
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
        /// Links displayed on this page
        /// </summary>
        protected Listing<Link> Links { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            // Do this so when loading the next page the previous content isn't visible
            // (When navigating to the same page with different parameters, the entire 
            // page isn't disposed)
            Links = null;

            Links = await Reddit.Listings.GetTop(
                LinkListingHelpers.BuildSortListingParameters(After, Before, Count, Limit));
        }
    }
}
