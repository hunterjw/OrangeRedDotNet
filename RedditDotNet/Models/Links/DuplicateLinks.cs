using Newtonsoft.Json;
using RedditDotNet.Json;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.Models.Links
{
    /// <summary>
    /// Duplicate Link object
    /// </summary>
    [JsonConverter(typeof(DuplicateLinksConverter))]
    public class DuplicateLinks
    {
        /// <summary>
        /// The original Link requested to get duplicates for
        /// </summary>
        public Thing<Listing<Thing<Link>>> Originals { get; set; }

        /// <summary>
        /// Duplicate Links
        /// </summary>
        public Thing<Listing<Thing<Link>>> Duplicates { get; set; }
    }
}
