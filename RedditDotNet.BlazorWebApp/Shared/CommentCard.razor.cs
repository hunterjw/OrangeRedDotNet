using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Models.Comments;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Card for a single comment and its replies
    /// </summary>
    public partial class CommentCard
    {
        #region Parameters
        /// <summary>
        /// Comment to display
        /// </summary>
        [Parameter]
        public CommentBase Comment { get; set; }
        #endregion

        /// <summary>
        /// If the replies are collapsed or not
        /// </summary>
        protected bool RepliesCollapsed { get; set; } = false;

        /// <summary>
        /// On click event handler for the collapse button
        /// </summary>
        /// <param name="e"></param>
        protected void RepliesCollapsedButton_OnClick(MouseEventArgs e)
        {
            RepliesCollapsed = !RepliesCollapsed;
        }

        /// <summary>
        /// Double click event handler for the card
        /// </summary>
        /// <param name="e"></param>
        protected void Card_OnDblClick(MouseEventArgs e)
        {
            RepliesCollapsed = !RepliesCollapsed;
        }
    }
}
