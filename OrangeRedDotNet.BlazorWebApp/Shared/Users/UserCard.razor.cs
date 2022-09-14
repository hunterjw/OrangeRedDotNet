using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.BlazorWebApp.Shared.Multis;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Users
{
    /// <summary>
    /// User profile card component
    /// </summary>
    public partial class UserCard
    {
        /// <summary>
        /// Injected Modal service
        /// </summary>
        [Inject]
        public IModalService ModalService { get; set; }
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

        /// <summary>
        /// Account data
        /// </summary>
        [Parameter]
        public OrangeRedDotNet.Models.Account.AccountData AccountData { get; set; }

        /// <summary>
        /// User title
        /// </summary>
        protected string Title { get; set; } = null;
        /// <summary>
        /// User description
        /// </summary>
        protected string Description { get; set; } = null;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            // Title
            if (!string.IsNullOrWhiteSpace(AccountData?.Subreddit?.Title))
            {
                Title = AccountData.Subreddit.Title;
            }
            else
            {
                Title = AccountData.Name;
            }
            // Description
            if (!string.IsNullOrWhiteSpace(AccountData?.Subreddit?.PublicDescription))
            {
                Description = AccountData.Subreddit.PublicDescription;
            }
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
                parameters.Add("SubredditName", AccountData.Subreddit.DisplayName);
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
                                    SubredditName = AccountData.Subreddit.DisplayName
                                });
                            }
                            else
                            {
                                await reddit.Multis.DeleteSubreddit(multiState.MultiReddit.Data.Path, AccountData.Subreddit.DisplayName);
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
        /// OnClick event handler for the follow button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task FollowButton_OnClick()
        {
            try
            {
                if (AccountData.Subreddit != null)
                {
                    bool subscribed = AccountData.Subreddit.UserIsSubscriber ?? false;
                    await RedditService.GetClient().Subreddits.Subscribe(new()
                    {
                        SubredditName = AccountData.Subreddit.DisplayName,
                        Action = subscribed ? SubscribeAction.Unsubscribe : SubscribeAction.Subscribe
                    });
                    AccountData.Subreddit.UserIsSubscriber = !subscribed;
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error updating subscription"));
            }
        }
    }
}
