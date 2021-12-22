using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Shared;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    public partial class LinksComponent
    {
        #region Injected Services
        /// <summary>
        /// Reddit client
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }
        #endregion

        #region Query Parameters
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
        #endregion

        #region Route Parameters
        /// <summary>
        /// Subreddit to get links for
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }
        /// <summary>
        /// The listing type to display
        /// </summary>
        [Parameter]
        public string ListingType { get; set; }
        #endregion

        /// <summary>
        /// Links displayed on this page
        /// </summary>
        protected Listing<Link> Links { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            // Do this so when loading the next page the previous content isn't visible
            // (When navigating to the same page with different parameters, the entire 
            // page isn't disposed)
            Links = null;

            switch (ListingType)
            {
                case "best":
                    Subreddit = string.Empty;
                    Links = await Reddit.Listings.GetBest(
                        LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit));
                    break;
                case "hot":
                    Links = await Reddit.Listings.GetHot(
                        LinkListingHelpers.BuildLocationListingParameters(After, Before, Count, Limit),
                        Subreddit);
                    break;
                case "new":
                    Links = await Reddit.Listings.GetNew(
                        LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit),
                        Subreddit);
                    break;
                case "rising":
                    Links = await Reddit.Listings.GetRising(
                        LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit),
                        Subreddit);
                    break;
                case "controversial":
                    Links = await Reddit.Listings.GetControversial(
                        LinkListingHelpers.BuildSortListingParameters(After, Before, Count, Limit),
                        Subreddit);
                    break;
                case "top":
                    Links = await Reddit.Listings.GetTop(
                        LinkListingHelpers.BuildSortListingParameters(After, Before, Count, Limit),
                        Subreddit);
                    break;
                default:
                    if (!string.IsNullOrWhiteSpace(Subreddit))
                    {
                        Links = await Reddit.Listings.GetHot(
                            LinkListingHelpers.BuildLocationListingParameters(After, Before, Count, Limit),
                            Subreddit);
                    }
                    else
                    {
                        ListingType = "best";
                        Links = await Reddit.Listings.GetBest(
                            LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit));
                    }
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                var linkSubredditName = Links?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
                if (!string.IsNullOrWhiteSpace(linkSubredditName))
                {
                    Subreddit = linkSubredditName;
                }
            }
        }

        /// <summary>
        /// Helper function to get the relative URL for the current page
        /// </summary>
        /// <returns></returns>
        protected string GetRelativeUrl()
        {
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                return $"/r/{Subreddit}/{ListingType}";
            }
            return $"/{ListingType}";
        }

        /// <summary>
        /// Helper function to build the listing parameters for the current page
        /// </summary>
        /// <returns>ListingParameters object</returns>
        protected ListingParameters BuildListingParameters()
        {
            return ListingType switch
            {
                "best" => LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit),
                "hot" => LinkListingHelpers.BuildLocationListingParameters(After, Before, Count, Limit),
                "new" => LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit),
                "rising" => LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit),
                "controversial" => LinkListingHelpers.BuildSortListingParameters(After, Before, Count, Limit),
                "top" => LinkListingHelpers.BuildSortListingParameters(After, Before, Count, Limit),
                _ => string.IsNullOrWhiteSpace(Subreddit) ? 
                    LinkListingHelpers.BuildListingParameters(After, Before, Count, Limit) :
                    LinkListingHelpers.BuildLocationListingParameters(After, Before, Count, Limit)
            };
        }

        /// <summary>
        /// Get the current type of this listing
        /// </summary>
        /// <returns>Listing type</returns>
        protected LinkListingTabs GetListingType()
        {
            return ListingType switch
            {
                "best" => LinkListingTabs.Best,
                "hot" => LinkListingTabs.Hot,
                "new" => LinkListingTabs.New,
                "rising" => LinkListingTabs.Rising,
                "controversial" => LinkListingTabs.Controversial,
                "top" => LinkListingTabs.Top,
                _ => string.IsNullOrWhiteSpace(Subreddit) ? LinkListingTabs.Best : LinkListingTabs.Hot,
            };
        }

        /// <summary>
        /// Get a loading quip for the listing
        /// </summary>
        /// <returns>Loading quip string</returns>
        protected string GetLoadingQuip()
        {
            return ListingType switch
            {
                "best" => "Loading the best of Reddit...",
                "hot" => "Getting some hot posts straight from the oven...",
                "new" => "Getting the latest posts straight from the source...",
                "rising" => "Getting the posts that rise to the top...",
                "controversial" => "Getting the spiciest posts...",
                "top" => "Getting the tippity top of Reddit...",
                _ => string.IsNullOrWhiteSpace(Subreddit) ? 
                    "Loading the best of Reddit..." : 
                    "Getting some hot posts straight from the oven...",
            };
        }
    }
}
