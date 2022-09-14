using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Parameters.Search;
using OrangeRedDotNet.Models.Search;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Search results page
    /// </summary>
    public partial class Search
    {
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
        /// Subreddit name
        /// </summary>
        [Parameter]
        public string Subreddit { get; set; }

        /// <summary>
        /// Search Query
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "q")]
        public string Query { get; set; }
        /// <summary>
        /// Search type
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "type")]
        public string SearchType { get; set; }
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
        /// <summary>
        /// Search sort
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Sort { get; set; }
        /// <summary>
        /// Restrict subreddit or not
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "restrict_sr")]
        public bool? RestrictSubreddit { get; set; }

        /// <summary>
        /// Search results
        /// </summary>
        protected SearchResults SearchResults { get; set; } = null;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                SearchResults = null;
                if (!string.IsNullOrWhiteSpace(Subreddit) 
                    && !Subreddit.IsSpecialSubreddit() 
                    && RestrictSubreddit == null)
                {
                    RestrictSubreddit = true;
                }

                var client = RedditService.GetClient();
                SearchResults = await client.Search.Search(BuildSearchListingParameters(), Subreddit);
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading search results"));
            }
        }

        /// <summary>
        /// Get the current search type
        /// </summary>
        /// <returns>Search type</returns>
        protected SearchType GetSearchType()
        {
            return string.IsNullOrWhiteSpace(SearchType) ?
                OrangeRedDotNet.Models.Parameters.Search.SearchType.Link :
                SearchType.ToEnumFromDescriptionString<SearchType>();
        }

        /// <summary>
        /// Get the current timescale
        /// </summary>
        /// <returns>Timescale</returns>
        protected Timescale? GetTimescale()
        {
            if (GetSearchType() != OrangeRedDotNet.Models.Parameters.Search.SearchType.Link)
            {
                return null;
            }
            return string.IsNullOrWhiteSpace(Timescale) ? OrangeRedDotNet.Models.Parameters.Timescale.All
                    : Timescale.ToEnumFromDescriptionString<Timescale>();
        }

        /// <summary>
        /// Get the current search sort
        /// </summary>
        /// <returns>Search sort</returns>
        protected SearchSort? GetSearchSort()
        {
            if (GetSearchType() != OrangeRedDotNet.Models.Parameters.Search.SearchType.Link)
            {
                return null;
            }
            return string.IsNullOrWhiteSpace(Sort) ? SearchSort.Relavance
                    : Sort.ToEnumFromDescriptionString<SearchSort>();
        }

        /// <summary>
        /// Builds a search listing parameters object from the current page parameters
        /// </summary>
        /// <returns>Search listing parameters object</returns>
        protected SearchListingParameters BuildSearchListingParameters()
        {
            return new SearchListingParameters
            {
                Query = Query,
                Type = GetSearchType(),
                After = After,
                Before = Before,
                Count = Count ?? 0,
                Limit = Limit ?? 25,
                Timescale = GetTimescale(),
                Sort = GetSearchSort(),
                RestrictSubreddit = RestrictSubreddit,
            };
        }

        /// <summary>
        /// Builds a listing parameters object from the current page parameters
        /// </summary>
        /// <returns>Listing parameters object</returns>
        protected ListingParameters BuildListingParameters()
        {
            return BuildSearchListingParameters();
        }

        /// <summary>
        /// Get the relative URL for the page
        /// </summary>
        /// <returns>Relative URL</returns>
        protected string GetRelativeUrl()
        {
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                return $"/r/{Subreddit}/search";
            }
            return "/search";
        }

        /// <summary>
        /// Get the after value for the next page
        /// </summary>
        /// <returns>After value</returns>
        protected string GetAfter()
        {
            return GetSearchType() switch
            {
                OrangeRedDotNet.Models.Parameters.Search.SearchType.Link => SearchResults?.Links?.Data?.After,
                OrangeRedDotNet.Models.Parameters.Search.SearchType.Subreddit => SearchResults?.Subreddits?.Data?.After,
                OrangeRedDotNet.Models.Parameters.Search.SearchType.User => SearchResults?.Users?.Data?.After,
                _ => null,
            };
        }

        /// <summary>
        /// Get the before value for the previous page
        /// </summary>
        /// <returns>Before value</returns>
        protected string GetBefore()
        {
            return GetSearchType() switch
            {
                OrangeRedDotNet.Models.Parameters.Search.SearchType.Link => SearchResults?.Links?.Data?.Before,
                OrangeRedDotNet.Models.Parameters.Search.SearchType.Subreddit => SearchResults?.Subreddits?.Data?.Before,
                OrangeRedDotNet.Models.Parameters.Search.SearchType.User => SearchResults?.Users?.Data?.Before,
                _ => null,
            };
        }

        /// <summary>
        /// OnChange event handler for the timescale select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task TimescaleSelect_OnChange(ChangeEventArgs args)
        {
            SearchListingParameters parameters = BuildSearchListingParameters();
            parameters.Timescale = ((string)args.Value).ToEnumFromDescriptionString<Timescale>();
            // reset the position of the of the listing
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }

        /// <summary>
        /// OnChange event handler for the sort select dropdown
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task SearchSortSelect_OnChange(ChangeEventArgs args)
        {
            SearchListingParameters parameters = BuildSearchListingParameters();
            parameters.Sort = ((string)args.Value).ToEnumFromDescriptionString<SearchSort>();
            // reset the position of the of the listing
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }

        /// <summary>
        /// OnChange event handler for the restrict subreddit switch
        /// </summary>
        /// <param name="args">ChangeEventArgs</param>
        /// <returns>awaitable task</returns>
        protected async Task RestrictSubredditSwitch_OnChange(ChangeEventArgs args)
        {
            SearchListingParameters parameters = BuildSearchListingParameters();
            parameters.RestrictSubreddit = (bool?)args.Value;
            // reset the position of the of the listing
            parameters.After = parameters.Before = null;
            parameters.Count = 0;
            string parametersString = await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync();
            NavigationManager.NavigateTo($"{GetRelativeUrl()}?{parametersString}");
        }
    }
}
