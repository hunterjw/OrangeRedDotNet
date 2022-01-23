using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared.Users
{
    /// <summary>
    /// User profile card component
    /// </summary>
    public partial class UserCard
    {
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
    }
}
