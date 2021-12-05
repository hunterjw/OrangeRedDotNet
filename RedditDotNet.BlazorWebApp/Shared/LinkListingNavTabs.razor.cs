using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared
{
    public enum LinkListingTabs
    {
        Best,
        Hot,
        New,
        Rising,
        Controversial,
        Top
    }

    public partial class LinkListingNavTabs
    {
        [Parameter]
        public LinkListingTabs ActiveTab { get; set; }
    }
}
