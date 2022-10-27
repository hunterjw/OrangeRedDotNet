using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using OrangeRedDotNet.Models.Parameters.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Comments
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
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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
        /// <summary>
        /// If the parent link is locked or not
        /// </summary>
        [Parameter]
        public bool ParentLinkLocked { get; set; }
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
        /// If the new comment card should be shown or not
        /// </summary>
        protected bool ShowNewCommentCard { get; set; } = false;

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
                        new()
                        {
                            LinkFullName = LinkFullName,
                            Children = children,
                            Sort = CommentSort
                        });
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
                    ThingParameters parameters = new()
                    {
                        Id = commentData.Name
                    };
                    if (commentData.Saved)
                    {
                        await client.LinksAndComments.Unsave(parameters);
                    }
                    else
                    {
                        await client.LinksAndComments.Save(parameters);
                    }
                    commentData.Saved = !commentData.Saved;
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Failed to update link save state"));
            }
        }

        /// <summary>
        /// Handler for when a new comment has been created
        /// </summary>
        /// <param name="comments">New comment(s)</param>
        protected void OnNewComments(List<CommentBase> comments)
        {
            if (Comment.Data is CommentData commentData)
            {
                if (commentData.Replies?.Data?.Children == null)
                {
                    commentData.Replies = new Listing<CommentBase>
                    {
                        Kind = "Listing",
                        Data = new ListingData<CommentBase>
                        {
                            Children = new List<CommentBase>(),
                            Count = 0                            
                        }
                    };
                }
                commentData.Replies.Data.Children.InsertRange(0, comments);
                commentData.Replies.Data.Count += comments.Count;
                ShowNewCommentCard = false;
            }
        }

        /// <summary>
        /// If a comment can be posted or not
        /// </summary>
        /// <returns>Boolean</returns>
        protected bool CanPostComment()
        {
            if (Comment.Data is CommentData commentData)
            {
                return RedditService.LoggedIn && !commentData.Archived && !commentData.Locked && !commentData.AuthorIsBlocked && !ParentLinkLocked;
            }
            return false;
        }
    }
}
