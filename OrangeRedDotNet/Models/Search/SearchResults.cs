using Newtonsoft.Json;
using OrangeRedDotNet.Json;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Subreddits;

namespace OrangeRedDotNet.Models.Search
{
    /// <summary>
    /// Search results object
    /// </summary>
    [JsonConverter(typeof(SearchResultsConverter))]
    public class SearchResults
    {
        /// <summary>
        /// Subreddits
        /// </summary>
        public Listing<Subreddit> Subreddits { get; set; }
        /// <summary>
        /// Links
        /// </summary>
        public Listing<Link> Links { get; set; }
        /// <summary>
        /// Users
        /// </summary>
        public Listing<Account.Account> Users { get; set; }
    }
}
