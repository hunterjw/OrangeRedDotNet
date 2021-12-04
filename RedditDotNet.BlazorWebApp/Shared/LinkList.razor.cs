using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared
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
        public Listing<Models.Links.Link> Links { get; set; }
    }
}
