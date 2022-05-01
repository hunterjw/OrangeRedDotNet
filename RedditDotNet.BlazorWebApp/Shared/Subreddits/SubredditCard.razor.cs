using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Shared.Multis;
using RedditDotNet.Exceptions;
using RedditDotNet.Models.Parameters;
using RedditDotNet.Models.Subreddits;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace RedditDotNet.BlazorWebApp.Shared.Subreddits
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
        /// Subreddit details
        /// </summary>
        [Parameter]
        public Subreddit SubredditDetails { get; set; }

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
            // TODO replace this with locally hosted resource
            return "https://via.placeholder.com/256";
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
                IModalReference modal = ModalService.Show<AddToMultiRedditModal>("Add to MultiReddit", parameters);
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
                                await reddit.Multis.AddSubreddit(multiState.MultiReddit.Data.Path, SubredditDetails.Data.DisplayName);
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
                await RedditService.GetClient().Subreddits.Subscribe(
                    SubredditDetails.Data.DisplayName, 
                    subscribed ? SubscribeAction.Unsubscribe : SubscribeAction.Subscribe);
                SubredditDetails.Data.UserIsSubscriber = !subscribed;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error updating subscription"));
            }
        }
    }
}
