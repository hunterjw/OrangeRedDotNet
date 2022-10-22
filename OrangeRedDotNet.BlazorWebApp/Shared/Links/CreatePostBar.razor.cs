using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Bar to display is link listings to create a new post
    /// </summary>
    public partial class CreatePostBar
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Subreddit to create a post in
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }

        /// <summary>
        /// Build a URL to the submit page
        /// </summary>
        /// <param name="kind">Kind of post to create</param>
        /// <returns>Relative URL</returns>
        protected string BuildUrl(SubmitKind? kind = null)
        {
            var url = string.Empty;
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                url += $"/r/{Subreddit}";
            }
            url += "/submit";
            if (kind.HasValue)
            {
                url += $"?kind={kind.Value.ToDescriptionString()}";
            }
            return url;
        }
    }
}
