using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Links;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Display comments for an article
    /// </summary>
    public partial class Comments
    {
        #region Injected services
        /// <summary>
        /// Reddit client
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }
        #endregion

        #region Route parameters
        /// <summary>
        /// Subreddit name
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }

        /// <summary>
        /// Article
        /// </summary>
        [Parameter]
        public string ArticleId { get; set; }
        #endregion

        /// <summary>
        /// Link(s) with comments
        /// </summary>
        protected LinkWithComments LinkWithComments { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            LinkWithComments = null;

            LinkWithComments = await Reddit.Listings.GetComments(
                ArticleId, 
                subreddit: Subreddit);

            var linkSubredditName = LinkWithComments?.Links?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
            if (!string.IsNullOrWhiteSpace(linkSubredditName))
            {
                Subreddit = linkSubredditName;
            }
        }
    }
}
