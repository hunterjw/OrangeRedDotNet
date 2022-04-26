using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Parameters;

namespace RedditDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Subreddits listing nav tabs
    /// </summary>
    public partial class SubredditsNavTabs
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// The active MySubredditsType
        /// </summary>
        [Parameter]
        public MySubredditsType? ActiveMySubredditsTab { get; set; }
        /// <summary>
        /// The active SubredditsType
        /// </summary>
        [Parameter]
        public SubredditsType? ActiveSubredditsTab { get; set; }
    }
}
