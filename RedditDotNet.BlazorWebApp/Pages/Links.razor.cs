using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    public partial class Links
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
        /// <summary>
        /// Username (for MultiReddits)
        /// </summary>
        [Parameter]
        public string UserName { get; set; }
        /// <summary>
        /// MultiReddit name
        /// </summary>
        [Parameter]
        public string MultiName { get; set; }
        #endregion

        /// <summary>
        /// Links displayed on this page
        /// </summary>
        protected Listing<Link> LinkListing { get; set; }

        /// <summary>
        /// Current user identity
        /// </summary>
        protected AccountData Identity { get; set; }

        /// <summary>
        /// If the listing is a MultiReddit or not
        /// </summary>
        protected bool IsMultiReddit => !string.IsNullOrWhiteSpace(MultiName);

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            // Do this so when loading the next page the previous content isn't visible
            // (When navigating to the same page with different parameters, the entire 
            // page isn't disposed)
            LinkListing = null;

            if (Identity == null)
            {
                Identity = await Reddit.Account.GetIdentity();
            }

            var listingTypeEnum = GetListingType();
            if (string.IsNullOrWhiteSpace(ListingType))
            {
                if (listingTypeEnum == LinkListingType.Best)
                {
                    // This means we're on the home page, which is "best"
                    ListingType = "best";
                }
                else if (listingTypeEnum == LinkListingType.Hot)
                {
                    // This means we're on a subreddit landing page, which is "hot"
                    ListingType = "hot";
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            switch (listingTypeEnum)
            {
                case LinkListingType.Best:
                    Subreddit = string.Empty;
                    LinkListing = await Reddit.Listings.GetBest(BuildListingParameters());
                    break;
                case LinkListingType.Hot:
                    LinkListing = IsMultiReddit ?
                        await Reddit.Listings.GetHot(GetMultiRedditUrl(), BuildLocationListingParameters()) : 
                        await Reddit.Listings.GetHot(BuildLocationListingParameters(), Subreddit);
                    break;
                case LinkListingType.New:
                    LinkListing = IsMultiReddit ?
                        await Reddit.Listings.GetNew(GetMultiRedditUrl(), BuildListingParameters()) : 
                        await Reddit.Listings.GetNew(BuildListingParameters(), Subreddit);
                    break;
                case LinkListingType.Rising:
                    LinkListing = IsMultiReddit ?
                        await Reddit.Listings.GetRising(GetMultiRedditUrl(), BuildListingParameters()) : 
                        await Reddit.Listings.GetRising(BuildListingParameters(), Subreddit);
                    break;
                case LinkListingType.Controversial:
                    LinkListing = IsMultiReddit ?
                        await Reddit.Listings.GetControversial(GetMultiRedditUrl(), BuildSortListingParameters()) : 
                        await Reddit.Listings.GetControversial(BuildSortListingParameters(), Subreddit);
                    break;
                case LinkListingType.Top:
                    LinkListing = IsMultiReddit ?
                        await Reddit.Listings.GetTop(GetMultiRedditUrl(), BuildSortListingParameters()) : 
                        await Reddit.Listings.GetTop(BuildSortListingParameters(), Subreddit);
                    break;
                default:
                    throw new ArgumentException();
            }

            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                var linkSubredditName = LinkListing?.Data?.Children?.FirstOrDefault()?.Data?.Subreddit;
                if (!string.IsNullOrWhiteSpace(linkSubredditName))
                {
                    Subreddit = linkSubredditName;
                }
            }
        }

        /// <summary>
        /// Get the MultiReddit base URL
        /// </summary>
        /// <returns>MultiReddit URL</returns>
        protected string GetMultiRedditUrl()
        {
            if (IsMultiReddit)
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    if (UserName.Equals(Identity?.Name))
                    {
                        return $"/me/m/{MultiName}";
                    }
                    return $"/user/{UserName}/m/{MultiName}";
                }
                return $"/me/m/{MultiName}";
            }
            return string.Empty;
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
            else if (IsMultiReddit)
            {
                return $"{GetMultiRedditUrl()}/{ListingType}";
            }
            return $"/{ListingType}";
        }

        /// <summary>
        /// Helper function to build the parameters object for the current page
        /// </summary>
        /// <returns>Parameters object</returns>
        protected ListingParameters BuildParameters()
        {
            return GetListingType() switch
            {
                LinkListingType.Best => BuildListingParameters(),
                LinkListingType.Hot => BuildLocationListingParameters(),
                LinkListingType.New => BuildListingParameters(),
                LinkListingType.Rising => BuildListingParameters(),
                LinkListingType.Controversial => BuildSortListingParameters(),
                LinkListingType.Top => BuildSortListingParameters(),
                _ => throw new ArgumentException()
            };
        }

        /// <summary>
        /// Get the current type of this listing
        /// </summary>
        /// <returns>Listing type</returns>
        protected LinkListingType GetListingType()
        {
            return ListingType switch
            {
                "best" => LinkListingType.Best,
                "hot" => LinkListingType.Hot,
                "new" => LinkListingType.New,
                "rising" => LinkListingType.Rising,
                "controversial" => LinkListingType.Controversial,
                "top" => LinkListingType.Top,
                _ => !string.IsNullOrWhiteSpace(Subreddit) || (IsMultiReddit) ? 
                    LinkListingType.Hot : LinkListingType.Best,
            };
        }

        /// <summary>
        /// Get a loading quip for the listing
        /// </summary>
        /// <returns>Loading quip string</returns>
        protected string GetLoadingQuip()
        {
            return GetListingType() switch
            {
                LinkListingType.Best => "Loading the best of Reddit...",
                LinkListingType.Hot => "Getting some hot posts straight from the oven...",
                LinkListingType.New => "Getting the latest posts straight from the source...",
                LinkListingType.Rising => "Getting the posts that rise to the top...",
                LinkListingType.Controversial => "Getting the spiciest posts...",
                LinkListingType.Top => "Getting the tippity top of Reddit...",
                _ => throw new ArgumentException()
            };
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
        /// Build a LocationListingParameters object based on the current component parameters
        /// </summary>
        /// <returns>LocationListingParameters object</returns>
        protected LocationListingParameters BuildLocationListingParameters()
        {
            return new LocationListingParameters
            {
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25
            };
        }

        /// <summary>
        /// Build a SortListingParameters object based on the current component parameters
        /// </summary>
        /// <returns>SortListingParameters object</returns>
        protected SortListingParameters BuildSortListingParameters()
        {
            return new SortListingParameters
            {
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25
            };
        }
    }
}
