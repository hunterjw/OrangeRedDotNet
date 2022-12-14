using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Parameters.Listings;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Pagination component for a Link listing
    /// </summary>
    public partial class ListingPagination
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Relative URL for the current listing (no query parameters
        /// </summary>
        [Parameter]
        public string RelativeUrl { get; set; }
        /// <summary>
        /// The fullname of the "before" Link
        /// </summary>
        [Parameter]
        public string Before { get; set; }
        /// <summary>
        /// The fullname of the "after" link
        /// </summary>
        [Parameter]
        public string After { get; set; }
        /// <summary>
        /// Current page parameters object
        /// </summary>
        [Parameter]
        public ListingParameters CurrentPageParameters { get; set; }

        /// <summary>
        /// Build the next page URL
        /// </summary>
        /// <returns>Relative URL for the next page</returns>
        protected async Task<string> BuildNextPageUrl()
        {
            var parameters = CurrentPageParameters.Copy();
            parameters.After = parameters.Before = null;
            parameters.After = After;
            if (string.IsNullOrWhiteSpace(CurrentPageParameters.After) && string.IsNullOrWhiteSpace(CurrentPageParameters.Before))
            {
                parameters.Count = parameters.Limit;
            }
            else if (!string.IsNullOrWhiteSpace(CurrentPageParameters.After))
            {
                parameters.Count += parameters.Limit;
            }
            return $"{RelativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }

        /// <summary>
        /// Build the previous page URL
        /// </summary>
        /// <returns>Relative URL for the previous page</returns>
        protected async Task<string> BuildPreviousPageUrl()
        {
            var parameters = CurrentPageParameters.Copy();
            parameters.After = parameters.Before = null;
            parameters.Before = Before;
            if (!string.IsNullOrWhiteSpace(CurrentPageParameters.Before))
            {
                parameters.Count -= parameters.Limit;
            }
            return $"{RelativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }
    }
}
