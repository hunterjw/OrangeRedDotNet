using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// Component for a list of comments
    /// </summary>
    public partial class CommentList
    {
        /// <summary>
        /// Listing of Links to display
        /// </summary>
        [Parameter]
        public Listing<CommentBase> Comments { get; set; }
    }
}
