using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Multis;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Subreddits;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    public partial class Links
    {
        #region Injected Services
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
        /// <summary>
        /// Timescale for the returned things
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "t")]
        public string Timescale { get; set; }
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
        /// If the listing is a MultiReddit or not
        /// </summary>
        protected bool IsMultiReddit => !string.IsNullOrWhiteSpace(MultiName);
        /// <summary>
        /// MultiReddit info
        /// </summary>
        protected MultiReddit MultiReddit { get; set; }
        /// <summary>
        /// If the multireddit data is loaded or not
        /// </summary>
        protected bool MultiRedditLoaded { get; set; }
        /// <summary>
        /// Subreddit details
        /// </summary>
        protected Subreddit SubredditDetails { get; set; }
        /// <summary>
        /// Subreddit rules
        /// </summary>
        protected RulesResponse Rules { get; set; }
        /// <summary>
        /// If the subreddit details are loaded or not
        /// </summary>
        protected bool SubredditDetailsLoaded { get; set; }
        /// <summary>
        /// If the listing is a subreddit or not
        /// </summary>
        protected bool IsSubreddit => !string.IsNullOrWhiteSpace(Subreddit);

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Reddit redditClient = RedditService.GetClient();

                // Do this so when loading the next page the previous content isn't visible
                // (When navigating to the same page with different parameters, the entire 
                // page isn't disposed)
                LinkListing = null;

                if (IsSubreddit)
                {
                    if (SubredditDetailsLoaded && !SubredditDetails.Data.DisplayName.Equals(Subreddit))
                    {
                        SubredditDetailsLoaded = false;
                    }
                    if (!SubredditDetailsLoaded && !Subreddit.IsSpecialSubreddit())
                    {
                        SubredditDetails = await redditClient.Subreddits.GetAbout(Subreddit);
                        Rules = await redditClient.Subreddits.GetRules(Subreddit);
                        SubredditDetailsLoaded = true;
                    }
                }
                else if (IsMultiReddit)
                {
                    if (MultiRedditLoaded && !GetMultiRedditUrl(true).Equals(MultiReddit.Data.Path.TrimEnd('/')))
                    {
                        MultiRedditLoaded = false;
                    }
                    if (!MultiRedditLoaded)
                    {
                        MultiReddit = await redditClient.Multis.GetMulti(GetMultiRedditUrl(true), true);
                        MultiRedditLoaded = true;
                    }
                }

                if (string.IsNullOrWhiteSpace(Timescale))
                {
                    Timescale = "hour";
                }

                if (IsSubreddit)
                {
                    LinkListing = await redditClient.Listings.GetLinksForSubreddit(
                        GetListingType().ToEnumFromDescriptionString<LinkListingType>(),
                        Subreddit,
                        BuildParameters());
                }
                else if (IsMultiReddit)
                {
                    LinkListing = await redditClient.Listings.GetLinksForMultireddit(
                        GetListingType().ToEnumFromDescriptionString<LinkListingType>(),
                        GetMultiRedditUrl(),
                        BuildParameters());
                }
                else
                {
                    LinkListing = await redditClient.Listings.GetLinks(
                        GetListingType().ToEnumFromDescriptionString<FrontPageListingType>(),
                        BuildParameters());
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading links"));
            }
        }

        /// <summary>
        /// Helper function to get the current listing type
        /// </summary>
        /// <returns>Listing type string</returns>
        protected string GetListingType()
        {
            if (string.IsNullOrWhiteSpace(ListingType))
            {
                if (IsSubreddit || IsMultiReddit)
                {
                    ListingType = "hot";
                }
                else
                {
                    ListingType = "best";
                }
            }
            return ListingType;
        }

        /// <summary>
        /// Get the MultiReddit base URL
        /// </summary>
        /// <returns>MultiReddit URL</returns>
        protected string GetMultiRedditUrl(bool forceLongForm = false)
        {
            if (IsMultiReddit)
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    if (UserName.Equals(RedditService.Identity?.Name) && !forceLongForm)
                    {
                        return $"/me/m/{MultiName}";
                    }
                    return $"/user/{UserName}/m/{MultiName}";
                }
                if (forceLongForm)
                {
                    return $"/user/{RedditService.Identity?.Name}/m/{MultiName}";
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
                return $"/r/{Subreddit}/{GetListingType()}";
            }
            else if (IsMultiReddit)
            {
                return $"{GetMultiRedditUrl()}/{GetListingType()}";
            }
            return $"/{GetListingType()}";
        }

        /// <summary>
        /// Helper function to build the parameters object for the current page
        /// </summary>
        /// <returns>Parameters object</returns>
        protected ListingParameters BuildParameters()
        {
            return GetListingType() switch
            {
                "best" => BuildListingParameters(),
                "hot" => BuildLocationListingParameters(),
                "new" => BuildListingParameters(),
                "rising" => BuildListingParameters(),
                "controversial" => BuildSortListingParameters(),
                "top" => BuildSortListingParameters(),
                _ => throw new ArgumentException()
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
                "best" => "Loading the best of Reddit...",
                "hot" => "Getting some hot posts straight from the oven...",
                "new" => "Getting the latest posts straight from the source...",
                "rising" => "Getting the posts that rise to the top...",
                "controversial" => "Getting the spiciest posts...",
                "top" => "Getting the tippity top of Reddit...",
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
                Limit = Limit ?? 25,
                Timescale = string.IsNullOrWhiteSpace(Timescale) ? OrangeRedDotNet.Models.Parameters.Timescale.Hour
                    : Timescale.ToEnumFromDescriptionString<Timescale>()
            };
        }

        /// <summary>
        /// OnChange event handler for the timescale select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task TimescaleSelect_OnChange(ChangeEventArgs args)
        {
            SortListingParameters parameters = BuildSortListingParameters();
            parameters.Timescale = ((string)args.Value).ToEnumFromDescriptionString<Timescale>();
            // reset the position of the of the listing
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }

        /// <summary>
        /// Event handler for when a multireddit is deleted
        /// </summary>
        /// <param name="multiPath">Multireddit path</param>
        protected void OnMultiRedditDelete(string multiPath)
        {
            NavigationManager.NavigateTo("");
        }

        /// <summary>
        /// Event handler for when a multireddit is copied
        /// </summary>
        /// <param name="multiReddit">Newly created multireddit</param>
        protected void OnMultiRedditCopy(MultiReddit multiReddit)
        {
            NavigationManager.NavigateTo(multiReddit.Data.Path);
        }
    }
}
