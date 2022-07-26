using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Models.Subreddits;

namespace RedditDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Card display for a single subreddit rule
    /// </summary>
    public partial class RuleCard
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        [Parameter]
        public Rule Rule { get; set; }
    }
}
