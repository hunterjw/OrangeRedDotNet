using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Home page of the web app
    /// </summary>
    public partial class Best
    {
        #region Injected Services
        /// <summary>
        /// Reddit client
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }
        /// <summary>
        /// HTTP Client
        /// </summary>
        [Inject]
        public HttpClient Http { get; set; }
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
        /// To show all or not (bypasses preferences that would hide results)
        /// </summary>
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
            
            Links = await Reddit.Listings.GetBest(BuildListingParameters());
            // TODO Remove testing code
            //var text = await Http.GetStringAsync("listing-sample.json");
            //Links = JsonConvert.DeserializeObject<Listing<Link>>(text);
        }

        /// <summary>
        /// Builds a ListingParameters object from the query parameters on this page
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
        /// Build the URL for the next page
        /// </summary>
        /// <returns>URL for the next page</returns>
        protected async Task<string> BuildNextPageUrl()
        {
            var parameters = BuildListingParameters();
            parameters.After = parameters.Before = null;
            parameters.Count += parameters.Limit;
            parameters.After = Links?.Data?.After;
            return $"/best?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }

        /// <summary>
        /// Build the URL for the previous page
        /// </summary>
        /// <returns>URL for the previous page</returns>
        protected async Task<string> BuildPreviousPageUrl()
        {
            var parameters = BuildListingParameters();
            parameters.After = parameters.Before = null;
            parameters.Count -= parameters.Limit;
            parameters.Before = Links?.Data?.Before;
            return $"/best?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }
    }
}
