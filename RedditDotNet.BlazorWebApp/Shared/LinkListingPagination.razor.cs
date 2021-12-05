using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared
{
    public partial class LinkListingPagination
    {
        [Parameter]
        public string RelativeUrl { get; set; }
        [Parameter]
        public string Before { get; set; }
        [Parameter]
        public string After { get; set; }
        [Parameter]
        public ListingParameters CurrentPageParameters { get; set; }
    }
}
