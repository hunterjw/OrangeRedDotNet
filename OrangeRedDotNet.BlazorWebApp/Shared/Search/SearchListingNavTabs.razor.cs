using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;
using System.Net.Http;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Parameters.Search;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Search
{
    /// <summary>
    /// Nav tabs for search results page
    /// </summary>
    public partial class SearchListingNavTabs
    {
        /// <summary>
        /// Navigation mangager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// The type of the active tab
        /// </summary>
        [Parameter]
        public string ActiveTab { get; set; }
        /// <summary>
        /// Subreddit to link to (optional)
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }
        /// <summary>
        /// Current page parameters object
        /// </summary>
        [Parameter]
        public SearchListingParameters CurrentPageParameters { get; set; }

        /// <summary>
        /// Get the prefix URL for a subreddit
        /// </summary>
        /// <returns>Prefix relative URL</returns>
        protected string GetRelativeUrlPrefix()
        {
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                return $"/r/{Subreddit}";
            }
            return string.Empty;
        }

        /// <summary>
        /// Navigate to a different tab
        /// </summary>
        /// <param name="name">Tab name</param>
        protected async Task NavigateTo(string name)
        {
            SearchListingParameters parameters = (SearchListingParameters)CurrentPageParameters.Copy();
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            parameters.Type = name.ToEnumFromDescriptionString<SearchType>();
            parameters.Sort = null;
            parameters.Timescale = null;
            NavigationManager.NavigateTo($"{GetRelativeUrlPrefix()}/search?" +
                $"{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}");
        }
    }
}
