using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.Interfaces;
using OrangeRedDotNet.Models.Listings;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    public partial class LinkOrCommentList
    {
        [Parameter]
        public Listing<ILinkOrComment> Listing { get; set; }
    }
}
