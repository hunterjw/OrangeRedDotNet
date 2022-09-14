using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Input group for voting
    /// </summary>
    public partial class VoteInput
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

        /// <summary>
        /// Current score on the votable thing
        /// </summary>
        [Parameter]
        public int Score { get; set; }

        /// <summary>
        /// If the current user likes the thing or not
        /// </summary>
        [Parameter]
        public bool? Likes { get; set; }

        /// <summary>
        /// If the score is hidden or not
        /// </summary>
        [Parameter]
        public bool ScoreHidden { get; set; }

        /// <summary>
        /// Id of the thing
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// If the thing is archived or not
        /// </summary>
        [Parameter]
        public bool Archived { get; set; }

        /// <summary>
        /// On click event handler for the up button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task UpButton_OnClick()
        {
            if (!RedditService.LoggedIn)
            {
                return;
            }
            if (!Likes.HasValue || !Likes.Value)
            {
                await Upvote();
            }
            else
            {
                await ClearVote();
            }
        }

        /// <summary>
        /// On click event handler for the down button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task DownButton_OnClick()
        {
            if (!RedditService.LoggedIn)
            {
                return;
            }
            if (!Likes.HasValue || Likes.Value)
            {
                await Downvote();
            }
            else
            {
                await ClearVote();
            }
        }

        /// <summary>
        /// Upvote a thing
        /// </summary>
        /// <returns>Awaitable task</returns>
        private async Task Upvote()
        {
            try
            {
                await RedditService.GetClient().LinksAndComments.Vote(new()
                {
                    Id = Id,
                    Dir = 1,
                    Rank = 2
                });
                Likes = true;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error upvoting"));
            }
        }

        /// <summary>
        /// Downvote a thing
        /// </summary>
        /// <returns>Awaitable task</returns>
        private async Task Downvote()
        {
            try
            {
                await RedditService.GetClient().LinksAndComments.Vote(new()
                {
                    Id = Id,
                    Dir = -1,
                    Rank = 2
                });
                Likes = false;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error downvoting"));
            }
        }

        /// <summary>
        /// Clear the vote on a thing
        /// </summary>
        /// <returns>Awaitable task</returns>
        private async Task ClearVote()
        {
            try
            {
                await RedditService.GetClient().LinksAndComments.Vote(new()
                {
                    Id = Id,
                    Dir = 0,
                    Rank = 2
                });
                Likes = null;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error clearing vote"));
            }
        }
    }
}
