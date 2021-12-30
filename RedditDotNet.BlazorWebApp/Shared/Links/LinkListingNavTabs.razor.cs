using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Navigation tabs for Link listings
    /// </summary>
    public partial class LinkListingNavTabs
    {
        /// <summary>
        /// The type of the active tab
        /// </summary>
        [Parameter]
        public LinkListingType ActiveTab { get; set; }

        /// <summary>
        /// Subreddit to link to (optional)
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }

        /// <summary>
        /// Get the prefix URL for a subreddit
        /// </summary>
        /// <returns>Prefix relative URL</returns>
        protected string GetSubredditPrefix()
        {
            if (string.IsNullOrWhiteSpace(Subreddit))
            {
                return string.Empty;
            }
            return $"/r/{Subreddit}";
        }
    }
}
