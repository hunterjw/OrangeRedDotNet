using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Shared.Multis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared.Users
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
        /// Account data
        /// </summary>
        [Parameter]
        public Models.Account.AccountData AccountData { get; set; }

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
            if (!RedditService.LoggedIn)
            {
                return;
            }
            Reddit reddit = RedditService.GetClient();
            var parameters = new ModalParameters();
            parameters.Add("SubredditName", AccountData.Subreddit.DisplayName);
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
                            await reddit.Multis.AddSubreddit(multiState.MultiReddit.Data.Path, AccountData.Subreddit.DisplayName);
                        }
                        else
                        {
                            await reddit.Multis.DeleteSubreddit(multiState.MultiReddit.Data.Path, AccountData.Subreddit.DisplayName);
                        }
                    }
                }
            }
        }
    }
}
