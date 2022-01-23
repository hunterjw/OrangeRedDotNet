using Microsoft.AspNetCore.Components;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared
{
    public partial class LinkOrCommentList
    {
        [Parameter]
        public Listing<ILinkOrComment> Listing { get; set; }
    }
}
