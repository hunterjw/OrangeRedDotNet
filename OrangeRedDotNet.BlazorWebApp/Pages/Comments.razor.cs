using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Subreddits;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Display comments for an article
    /// </summary>
    public partial class Comments
    {
        #region Injected services
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }
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

        #region Query Parameters
        /// <summary>
        /// Comment sort
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "sort")]
        public string Sort { get; set; }
        #endregion

        /// <summary>
        /// Link(s) with comments
        /// </summary>
        protected LinkWithComments LinkWithComments { get; set; }
        /// <summary>
        /// Subreddit details
        /// </summary>
        protected Subreddit SubredditDetails { get; set; }
        /// <summary>
        /// Subreddit details loaded
        /// </summary>
        protected bool SubredditDetailsLoaded { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var redditClient = RedditService.GetClient();

                LinkWithComments = null;

                if (SubredditDetailsLoaded && !SubredditDetails.Data.DisplayName.Equals(Subreddit))
                {
                    SubredditDetailsLoaded = false;
                }
                if (!SubredditDetailsLoaded && !Subreddit.IsSpecialSubreddit())
                {
                    SubredditDetails = await redditClient.Subreddits.GetAbout(Subreddit);
                    SubredditDetailsLoaded = true;
                }

                if (string.IsNullOrWhiteSpace(Sort))
                {
                    Sort = RedditService.Preferences?.DefaultCommentSort != null ?
                        RedditService.Preferences.DefaultCommentSort.ToDescriptionString() : 
                        "confidence";
                }

                LinkWithComments = await redditClient.Listings.GetComments(
                    ArticleId,
                    subreddit: Subreddit,
                    parameters: BuildCommentListingParameters());

                var linkSubredditName = LinkWithComments?.Links?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
                if (!string.IsNullOrWhiteSpace(linkSubredditName))
                {
                    Subreddit = linkSubredditName;
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading comments"));
            }
        }

        /// <summary>
        /// Build a CommentListingParameters object based on the current page parameters
        /// </summary>
        /// <returns>CommentListingParameters object</returns>
        protected CommentListingParameters BuildCommentListingParameters()
        {
            return new CommentListingParameters
            {
                Sort = string.IsNullOrWhiteSpace(Sort) ? null : Sort.ToEnumFromDescriptionString<CommentSort>()
            };
        }

        /// <summary>
        /// Get the relative URL for the current page
        /// </summary>
        /// <returns></returns>
        protected string GetRelativeUrl()
        {
            return $"/r/{Subreddit}/comments/{ArticleId}";
        }

        /// <summary>
        /// OnChange event handler for the sort select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task SortSelect_OnChange(ChangeEventArgs args)
        {
            CommentListingParameters parameters = BuildCommentListingParameters();
            parameters.Sort = ((string)args.Value).ToEnumFromDescriptionString<CommentSort>();
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }
    }
}
