using Newtonsoft.Json;
using RedditDotNet.Json;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Subreddits;

namespace RedditDotNet.Models.Search
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
