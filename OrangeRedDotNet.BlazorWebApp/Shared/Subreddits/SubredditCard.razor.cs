using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.BlazorWebApp.Shared.Multis;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using OrangeRedDotNet.Models.Subreddits;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Subreddit card
    /// </summary>
    public partial class SubredditCard
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Modal service
        /// </summary>
        [Inject]
        public IModalService ModalService { get; set; }
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

        /// <summary>
        /// Subreddit details
        /// </summary>
        [Parameter]
        public Subreddit SubredditDetails { get; set; }
        /// <summary>
        /// If the footer should be hidden or not
        /// </summary>
        [Parameter]
        public bool HideFooter { get; set; }

        /// <summary>
        /// If the content is collapsed
        /// </summary>
        protected bool ContentCollapsed { get; set; } = true;
        /// <summary>
        /// If the subreddit is a user profile
        /// </summary>
        protected bool IsUser
        {
            get
            {
                return SubredditDetails?.Data?.DisplayName?.StartsWith("u_") ?? false;
            }
        }

        /// <summary>
        /// Get the icon URL for this subreddit
        /// </summary>
        /// <returns>Image URL</returns>
        protected string GetIconUrl()
        {
            if (!string.IsNullOrWhiteSpace(SubredditDetails.Data.IconImg))
            {
                return HttpUtility.HtmlDecode(SubredditDetails.Data.IconImg);
            }
            else if (!string.IsNullOrWhiteSpace(SubredditDetails.Data.CommunityIcon))
            {
                return HttpUtility.HtmlDecode(SubredditDetails.Data.CommunityIcon);
            }
            return "icon-512.png";
        }

        /// <summary>
        /// Get the tile for the subreddit
        /// </summary>
        /// <returns></returns>
        protected string GetTitle()
        {
            if (!string.IsNullOrWhiteSpace(SubredditDetails?.Data?.Title))
            {
                return SubredditDetails.Data.Title;
            }
            return SubredditDetails.Data.DisplayName;
        }

        /// <summary>
        /// OnClick event handler for the add to multireddit button
        /// </summary>
        protected async Task AddToMultiRedditButton_OnClick()
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
                Reddit reddit = RedditService.GetClient();
                var parameters = new ModalParameters();
                parameters.Add("SubredditName", SubredditDetails.Data.DisplayName);
                ModalOptions options = new()
                {
                    Class = $"blazored-modal {(ThemeService.AppTheme.DarkMode ? "dark-modal" : "")}"
                };
                IModalReference modal = ModalService.Show<AddToMultiRedditModal>("Add to MultiReddit", parameters, options);
                ModalResult result = await modal.Result;
                if (!result.Cancelled)
                {
                    var multiRedditStates = result.Data as List<MultiRedditState>;
                    foreach (var multiState in multiRedditStates)
                    {
                        if (multiState.OriginalState != multiState.CurrentState)
                        {
                            if (multiState.CurrentState)
                            {
                                await reddit.Multis.AddSubreddit(multiState.MultiReddit.Data.Path, new()
                                {
                                    SubredditName = SubredditDetails.Data.DisplayName
                                });
                            }
                            else
                            {
                                await reddit.Multis.DeleteSubreddit(multiState.MultiReddit.Data.Path, SubredditDetails.Data.DisplayName);
                            }
                        }
                    }
                    ToastService.ShowSuccess("Updated multireddits");
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error updating multireddits"));
            }
        }

        /// <summary>
        /// OnClick event handler for the subscribe button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task SubscribeButton_OnClick()
        {
            try
            {
                bool subscribed = SubredditDetails.Data.UserIsSubscriber ?? false;
                await RedditService.GetClient().Subreddits.Subscribe(new()
                {
                    SubredditName = SubredditDetails.Data.DisplayName,
                    Action = subscribed ? SubscribeAction.Unsubscribe : SubscribeAction.Subscribe
                });
                SubredditDetails.Data.UserIsSubscriber = !subscribed;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error updating subscription"));
            }
        }
    }
}
