using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Models.Comments;

namespace RedditDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// Card for displaying a comment referenced elsewhere
    /// (like the user overview listing)
    /// </summary>
    public partial class CommentRefCard
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Comment to display
        /// </summary>
        [Parameter]
        public CommentBase Comment { get; set; }
    }
}
