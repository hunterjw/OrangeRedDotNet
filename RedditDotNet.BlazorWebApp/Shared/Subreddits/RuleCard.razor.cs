using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Subreddits;

namespace RedditDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Card display for a single subreddit rule
    /// </summary>
    public partial class RuleCard
    {
        [Parameter]
        public Rule Rule { get; set; }
    }
}
