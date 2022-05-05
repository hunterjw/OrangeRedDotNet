using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// Card for a single comment and its replies
    /// </summary>
    public partial class CommentCard
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }

        #region Parameters
        /// <summary>
        /// Comment to display
        /// </summary>
        [Parameter]
        public CommentBase Comment { get; set; }
        /// <summary>
        /// The original poster for the parent link that this comment belongs to
        /// </summary>
        [Parameter]
        public string OriginalPoster { get; set; }
        /// <summary>
        /// Full name of the link this comment belongs to
        /// </summary>
        [Parameter]
        public string LinkFullName { get; set; }
        /// <summary>
        /// Current sort of the comments
        /// </summary>
        [Parameter]
        public CommentSort? CommentSort { get; set; }
        #endregion

        /// <summary>
        /// If the replies are collapsed or not
        /// </summary>
        protected bool RepliesCollapsed { get; set; } = false;
        /// <summary>
        /// If the "more comments" section is loaded
        /// </summary>
        protected bool MoreLoaded { get; set; } = true;
        /// <summary>
        /// Additional comments loaded
        /// </summary>
        protected List<CommentBase> MoreComments { get; set; } = new List<CommentBase>();

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

        /// <summary>
        /// OnClick event handler for the MoreCommentsButton
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task MoreCommentsButton_OnClick()
        {
            try
            {
                MoreLoaded = false;
                if (Comment.Data is MoreData moreData)
                {
                    var numChildren = Math.Min(moreData.Children.Count, 100);
                    var children = moreData.Children.Take(numChildren).ToList();
                    moreData.Children.RemoveRange(0, numChildren);
                    var result = await RedditService.GetClient().LinksAndComments.GetMoreChildren(
                        LinkFullName, children, sort: CommentSort);
                    moreData.Count -= result.Count;
                    MoreComments.AddRange(result.NestComments(moreData.ParentId));
                }
                MoreLoaded = true;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading more comments"));
            }
        }

        /// <summary>
        /// OnClick handler for the SaveToggleButton
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task SaveToggleButton_OnClick()
        {
            try
            {
                if (Comment.Data is CommentData commentData)
                {
                    Reddit client = RedditService.GetClient();
                    if (commentData.Saved)
                    {
                        await client.LinksAndComments.Unsave(commentData.Name);
                    }
                    else
                    {
                        await client.LinksAndComments.Save(commentData.Name);
                    }
                    commentData.Saved = !commentData.Saved;
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Failed to update link save state"));
            }
        }
    }
}
