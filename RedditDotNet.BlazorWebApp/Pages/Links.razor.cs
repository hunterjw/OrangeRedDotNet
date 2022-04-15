﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.Exceptions;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Multis;
using RedditDotNet.Models.Parameters;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
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

                if (IsMultiReddit)
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
                        LinkListing = await redditClient.Listings.GetBest(BuildListingParameters());
                        break;
                    case LinkListingType.Hot:
                        LinkListing = IsMultiReddit ?
                            await redditClient.Listings.GetHot(GetMultiRedditUrl(), BuildLocationListingParameters()) :
                            await redditClient.Listings.GetHot(BuildLocationListingParameters(), Subreddit);
                        break;
                    case LinkListingType.New:
                        LinkListing = IsMultiReddit ?
                            await redditClient.Listings.GetNew(GetMultiRedditUrl(), BuildListingParameters()) :
                            await redditClient.Listings.GetNew(BuildListingParameters(), Subreddit);
                        break;
                    case LinkListingType.Rising:
                        LinkListing = IsMultiReddit ?
                            await redditClient.Listings.GetRising(GetMultiRedditUrl(), BuildListingParameters()) :
                            await redditClient.Listings.GetRising(BuildListingParameters(), Subreddit);
                        break;
                    case LinkListingType.Controversial:
                        LinkListing = IsMultiReddit ?
                            await redditClient.Listings.GetControversial(GetMultiRedditUrl(), BuildSortListingParameters()) :
                            await redditClient.Listings.GetControversial(BuildSortListingParameters(), Subreddit);
                        break;
                    case LinkListingType.Top:
                        LinkListing = IsMultiReddit ?
                            await redditClient.Listings.GetTop(GetMultiRedditUrl(), BuildSortListingParameters()) :
                            await redditClient.Listings.GetTop(BuildSortListingParameters(), Subreddit);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading links"));
            }
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
                Limit = Limit ?? 25,
                Timescale = string.IsNullOrWhiteSpace(Timescale) ? Models.Parameters.Timescale.Hour 
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
