using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Parameters.Subreddits;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Subreddits
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
        /// Navigation Manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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

        /// <summary>
        /// Get the active tab name
        /// </summary>
        /// <returns>Active tab name</returns>
        protected string GetActiveTabName()
        {
            return ActiveMySubredditsTab != null ? 
                ActiveMySubredditsTab.ToString() : 
                (ActiveSubredditsTab != null ? 
                    ActiveSubredditsTab.ToString() : 
                    SubredditsType.Popular.ToString());
        }

        /// <summary>
        /// Navigate to a new tab
        /// </summary>
        /// <param name="subredditType">Tab to navigate to</param>
        protected void NavigateTo(SubredditsType subredditType)
        {
            switch (subredditType)
            {
                case SubredditsType.Popular:
                    NavigationManager.NavigateTo("/subreddits/popular");
                    break;
                case SubredditsType.New:
                    NavigationManager.NavigateTo("/subreddits/new");
                    break;
                case SubredditsType.Default:
                    NavigationManager.NavigateTo("/subreddits/default");
                    break;
            }
        }

        /// <summary>
        /// Navigate to a new tab
        /// </summary>
        /// <param name="mySubredditType">Tab to navigate to</param>
        protected void NavigateTo(MySubredditsType mySubredditType)
        {
            switch (mySubredditType)
            {
                case MySubredditsType.Subscriber:
                    NavigationManager.NavigateTo("/subreddits/mine/subscriber");
                    break;
                case MySubredditsType.Contributor:
                    NavigationManager.NavigateTo("/subreddits/mine/contributor");
                    break;
                case MySubredditsType.Moderator:
                    NavigationManager.NavigateTo("/subreddits/mine/moderator");
                    break;
            }
        }
    }
}
