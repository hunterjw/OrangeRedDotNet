using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared
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
        public Reddit Reddit { get; set; }

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
            await Reddit.LinksAndComments.Vote(Id, 1, 2);
            Likes = true;
        }

        /// <summary>
        /// Downvote a thing
        /// </summary>
        /// <returns>Awaitable task</returns>
        private async Task Downvote()
        {
            await Reddit.LinksAndComments.Vote(Id, -1, 2);
            Likes = false;
        }

        /// <summary>
        /// Clear the vote on a thing
        /// </summary>
        /// <returns>Awaitable task</returns>
        private async Task ClearVote()
        {
            await Reddit.LinksAndComments.Vote(Id, 0, 2);
            Likes = null;
        }
    }
}
