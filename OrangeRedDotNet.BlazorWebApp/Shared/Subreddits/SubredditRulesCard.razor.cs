using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Subreddits;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Card display for a set of subreddit rules
    /// </summary>
    public partial class SubredditRulesCard
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Subreddit rules
        /// </summary>
        [Parameter]
        public RulesResponse Rules { get; set; }

        /// <summary>
        /// If the content is collapsed
        /// </summary>
        protected bool ContentCollapsed { get; set; } = true;
    }
}
