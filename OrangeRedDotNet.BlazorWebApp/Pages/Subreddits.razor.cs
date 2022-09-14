using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using OrangeRedDotNet.Models.Subreddits;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    public partial class Subreddits
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }

        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string After { get; set; }
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Before { get; set; }
        /// <summary>
        /// Number of items already retrieved
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public int? Count { get; set; }
        /// <summary>
        /// Maximum number of things to return
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public int? Limit { get; set; }

        /// <summary>
        /// My subreddits type
        /// </summary>
        [Parameter]
        public string MySubredditsType { get; set; }
        /// <summary>
        /// Subreddits type
        /// </summary>
        [Parameter]
        public string SubredditsType { get; set; }

        /// <summary>
        /// Listing of subreddits
        /// </summary>
        protected Listing<Subreddit> SubredditsListing { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                SubredditsListing = null;

                if (string.IsNullOrWhiteSpace(SubredditsType) && 
                    string.IsNullOrWhiteSpace(MySubredditsType))
                {
                    SubredditsType = "popular";
                }

                if (!string.IsNullOrWhiteSpace(SubredditsType))
                {
                    SubredditsType subredditsType = GetSubredditsType() ??
                        OrangeRedDotNet.Models.Parameters.Subreddits.SubredditsType.Popular;

                    var redditClient = RedditService.GetClient();
                    SubredditsListing = await redditClient.Subreddits.Get(
                        subredditsType,
                        BuildListingParameters());
                }
                else
                {
                    MySubredditsType mySubredditsType = GetMySubredditsType() ??
                        OrangeRedDotNet.Models.Parameters.Subreddits.MySubredditsType.Subscriber;

                    var redditClient = RedditService.GetClient();
                    SubredditsListing = await redditClient.Subreddits.GetMine(
                        mySubredditsType,
                        BuildListingParameters());
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading subreddits"));
            }
        }

        /// <summary>
        /// Build a ListingParameters object based on the current component parameters
        /// </summary>
        /// <returns>ListingParameters object</returns>
        protected ListingParameters BuildListingParameters()
        {
            return new ListingParameters
            {
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25
            };
        }

        /// <summary>
        /// Get the relative URL for the current page
        /// </summary>
        /// <returns>Relative URL</returns>
        protected string GetRelativeUrl()
        {
            if (string.IsNullOrWhiteSpace(MySubredditsType) && 
                string.IsNullOrWhiteSpace(SubredditsType))
            {
                return "/subreddits/popular";
            }
            else if (!string.IsNullOrWhiteSpace(SubredditsType))
            {
                return $"/subreddits/{SubredditsType}";
            }
            else
            {
                return $"/subreddits/mine/{MySubredditsType}";
            }
        }

        /// <summary>
        /// Get MySubredditsType
        /// </summary>
        /// <returns>MySubredditsType, or null</returns>
        protected MySubredditsType? GetMySubredditsType()
        {
            if (string.IsNullOrWhiteSpace(MySubredditsType))
            {
                return null;
            }
            return MySubredditsType.ToEnumFromDescriptionString<MySubredditsType>();
        }

        /// <summary>
        /// Get SubredditsType
        /// </summary>
        /// <returns>SubredditsType, or null</returns>
        protected SubredditsType? GetSubredditsType()
        {
            if (string.IsNullOrWhiteSpace(SubredditsType))
            {
                return null;
            }
            return SubredditsType.ToEnumFromDescriptionString<SubredditsType>();
        }
    }
}
