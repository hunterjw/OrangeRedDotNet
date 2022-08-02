using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using OrangeRedDotNet.BlazorWebApp.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Main Nav Menu component
    /// </summary>
    public partial class NavMenu
    {
        /// <summary>
        /// Regex for matching subreddit name
        /// </summary>
        private static readonly Regex _subredditRegex = new("^/r/(\\w+)");
        /// <summary>
        /// Regex for matching search relative URL
        /// </summary>
        private static readonly Regex _searchRegex = new("^(?:/r/(?:\\w+))?/search");

        /// <summary>
        /// Reddit Service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Configuration
        /// </summary>
        [Inject]
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// If the nav menu is collapsed or not
        /// </summary>
        protected bool NavMenuCollapsed { get; set; } = false;
        /// <summary>
        /// Current subreddit
        /// </summary>
        protected string Subreddit { get; set; } = string.Empty;
        /// <summary>
        /// Current search query
        /// </summary>
        protected string SearchQuery { get; set; } = string.Empty;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            UpdateLocation(NavigationManager.Uri);
            ThemeService.ThemeChanged += RefreshComponent;
            RedditService.LoginFinished += RefreshComponent;
            RedditService.LogoutFinished += RefreshComponent;
            NavigationManager.LocationChanged += LocationChanged;
        }

        /// <summary>
        /// Handler for when to refresh this component
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Args object</param>
        protected void RefreshComponent(object sender, object args)
        {
            StateHasChanged();
        }

        /// <summary>
        /// Location changed handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Args</param>
        protected void LocationChanged(object sender, LocationChangedEventArgs args)
        {
            UpdateLocation(args.Location);
            RefreshComponent(sender, args);
        }

        /// <summary>
        /// Update the current location
        /// </summary>
        /// <param name="newLocation">New location</param>
        protected void UpdateLocation(string newLocation)
        {
            var location = newLocation[(NavigationManager.BaseUri.Length - 1)..];

            var subredditMatch = _subredditRegex.Match(location);
            if (subredditMatch.Success)
            {
                Subreddit = subredditMatch.Groups[1].Value;
            }
            else
            {
                Subreddit = string.Empty;
            }

            if (!_searchRegex.Match(location).Success)
            {
                SearchQuery = string.Empty;
            }
        }

        /// <summary>
        /// Handle running a search
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task SearchHandler()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Dictionary<string, string> parameters = new()
                {
                    { "q", SearchQuery }
                };
                NavigationManager.NavigateTo(
                    $"{(!string.IsNullOrWhiteSpace(Subreddit) ? $"/r/{Subreddit}" : "")}" +
                    $"/search?{await new FormUrlEncodedContent(parameters).ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// On submit handler for the search text box
        /// </summary>
        /// <param name="args">Args</param>
        /// <returns>Awaitable task</returns>
        protected async Task OnSearchSubmit(KeyboardEventArgs args)
        {
            if (args.Key.Equals("Enter"))
            {
                await SearchHandler();
            }
        }
    }
}
