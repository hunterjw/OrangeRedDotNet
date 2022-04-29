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
    }
}
