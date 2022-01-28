using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Comments;

namespace RedditDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// Card for displaying a comment referenced elsewhere
    /// (like the user overview listing)
    /// </summary>
    public partial class CommentRefCard
    {
        #region Parameters
        /// <summary>
        /// Comment to display
        /// </summary>
        [Parameter]
        public CommentBase Comment { get; set; }
        #endregion
    }
}
