using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Home page of the web app
    /// </summary>
    public partial class Index
    {
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

        /// <summary>
        /// Links displayed on this page
        /// </summary>
        protected Listing<Link> Links { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Links = await Reddit.Listings.GetBest();
            // TODO Remove testing code
            //var text = await Http.GetStringAsync("listing-sample.json");
            //Links = JsonConvert.DeserializeObject<Listing<Link>>(text);
        }
    }
}
