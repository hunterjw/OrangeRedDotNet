using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Navigation tabs for Link listings
    /// </summary>
    public partial class LinkListingNavTabs
    {
        /// <summary>
        /// Navigation mangager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
        /// MultiReddit base path
        /// </summary>
        [Parameter]
        public string MultiRedditPath { get; set; }

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
            else if (!string.IsNullOrWhiteSpace(MultiRedditPath))
            {
                return MultiRedditPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// Navigate to a different tab
        /// </summary>
        /// <param name="name">Tab name</param>
        protected void NavigateTo(string name)
        {
            if (name.Equals("best"))
            {
                NavigationManager.NavigateTo($"/best");
            }
            NavigationManager.NavigateTo($"{GetRelativeUrlPrefix()}/{name}");
        }
    }
}
