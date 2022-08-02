using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Subreddits;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Component for displaying a list of Links
    /// </summary>
    public partial class LinkList
    {
        /// <summary>
        /// Listing of Links to display
        /// </summary>
        [Parameter]
        public Listing<OrangeRedDotNet.Models.Links.Link> Links { get; set; }
        /// <summary>
        /// If to have the link contents collapsed or not by default
        /// </summary>
        [Parameter]
        public bool ContentCollapsed { get; set; } = true;
        /// <summary>
        /// Subreddit context
        /// Should be populated when the link list is shown in a subreddit context
        /// </summary>
        [Parameter]
        public Subreddit Subreddit { get; set; }
    }
}
