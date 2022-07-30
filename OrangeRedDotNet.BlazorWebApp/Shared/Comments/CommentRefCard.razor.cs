using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Comments;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Comments
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
