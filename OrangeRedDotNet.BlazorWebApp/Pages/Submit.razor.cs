using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.LinksAndComments;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using System.Threading.Tasks;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Links;
using System.Linq;
using System;
using OrangeRedDotNet.Models.Subreddits;
using System.Collections.Generic;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Page to create a new post
    /// </summary>
    public partial class Submit
    {
        /// <summary>
        /// Get allowed post types
        /// </summary>
        /// <param name="subredditDetails">Subreddit details</param>
        /// <returns>Enumerable of submit kinds</returns>
        protected static IEnumerable<SubmitKind> GetAllowedPostTypes(Subreddit subredditDetails)
        {
            if (subredditDetails == null || subredditDetails.Data.SubmissionType == "any")
            {
                Enum.GetValues<SubmitKind>();
                return Enum.GetValues<SubmitKind>();
            }
            return new SubmitKind[]
            {
                subredditDetails.Data.SubmissionType.ToEnumFromDescriptionString<SubmitKind>()
            };
        }

        /// <summary>
        /// If spoilers are enabled or not
        /// </summary>
        /// <param name="subredditDetails">Subreddit details</param>
        /// <returns>True or false</returns>
        protected static bool SpoilersEnabled(Subreddit subredditDetails)
        {
            return subredditDetails?.Data?.SpoilersEnabled ?? false;
        }

        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
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
        /// Subreddit to create post in
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }

        /// <summary>
        /// Kind of post to create
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Kind { get; set; }

        /// <summary>
        /// Submit parameters
        /// </summary>
        protected SubmitParameters SubmitParameters { get; set; }
        /// <summary>
        /// IF we are trying to post right now
        /// </summary>
        protected bool Posting { get; set; } = false;
        /// <summary>
        /// Current subreddit details
        /// </summary>
        protected Subreddit SubredditDetails { get; set; }
        /// <summary>
        /// Current subreddit post requirements
        /// </summary>
        protected PostRequirements PostRequirements { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            SubmitParameters = new SubmitParameters
            {
                Kind = !string.IsNullOrWhiteSpace(Kind) ? Kind.ToEnumFromDescriptionString<SubmitKind>() : SubmitKind.Self,
                Subreddit = Subreddit
            };
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                await LoadSubredditDetails();
            }
        }

        /// <summary>
        /// OnClick event handler for the Post button
        /// </summary>
        protected async Task PostButton_OnClick()
        {
            if (!Posting)
            {
                try
                {
                    Posting = true;

                    SubmitParameters parameters = SubmitParameters.FilterParametersByKind(SubmitParameters.Kind);
                    SubmitResponse response = await RedditService.GetClient().LinksAndComments.Submit(parameters);
                    if (response?.Content?.Errors?.Count > 0)
                    {
                        string message = string.Join(Environment.NewLine, response.Content.Errors.Select(_ => string.Join(" - ", _)));
                        ToastService.ShowError(message, heading: "Error creating post");
                    }
                    else
                    {
                        Listing<Link> posts = await RedditService.GetClient().Listings.GetByIds(new[]
                        {
                            response.Content.Data.Name
                        });
                        Link post = posts.Data.Children.First();
                        string relativeUrl = $"/r/{post.Data.Subreddit}/comments/{post.Data.Id}";
                        NavigationManager.NavigateTo(relativeUrl);
                    }
                }
                catch (RedditApiException rex)
                {
                    ToastService.ShowError(rex.MakeErrorMessage("Error creating post"));
                }
                finally
                {
                    Posting = false;
                }
            }
        }

        /// <summary>
        /// Handler for when the selected tab changes
        /// </summary>
        /// <param name="name">Tab name</param>
        protected void OnSelectedTabChanged(string name)
        {
            SubmitParameters.Kind = name.ToEnumFromDescriptionString<SubmitKind>();
        }

        /// <summary>
        /// Clear subreddit details
        /// </summary>
        protected void ClearSubredditDetails()
        {
            SubredditDetails = null;
            PostRequirements = null;
            SubmitParameters.Spoiler = null;
        }

        /// <summary>
        /// Load subreddit details
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task LoadSubredditDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(SubmitParameters.Subreddit))
                {
                    ClearSubredditDetails();
                }
                else if (SubredditDetails == null || SubredditDetails?.Data?.DisplayName != SubmitParameters.Subreddit)
                {
                    Reddit client = RedditService.GetClient();
                    SubredditDetails = await client.Subreddits.GetAbout(SubmitParameters.Subreddit);
                    PostRequirements = await client.Subreddits.GetPostRequirements(SubmitParameters.Subreddit);
                    IEnumerable<SubmitKind> allowedPostTypes = GetAllowedPostTypes(SubredditDetails);
                    if (!allowedPostTypes.Contains(SubmitParameters.Kind))
                    {
                        SubmitParameters.Kind = allowedPostTypes.First();
                    }
                    if (!SpoilersEnabled(SubredditDetails))
                    {
                        SubmitParameters.Spoiler = null;
                    }
                }
            }
            catch
            {
                ClearSubredditDetails();
            }
        }
    }
}
