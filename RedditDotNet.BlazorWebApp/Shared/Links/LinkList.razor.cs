using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared.Links
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
        public Listing<RedditDotNet.Models.Links.Link> Links { get; set; }

        /// <summary>
        /// If to have the link contents collapsed or not by default
        /// </summary>
        [Parameter]
        public bool ContentCollapsed { get; set; } = true;
    }
}
