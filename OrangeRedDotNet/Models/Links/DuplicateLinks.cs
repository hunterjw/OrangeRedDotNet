using Newtonsoft.Json;
using OrangeRedDotNet.Json;
using OrangeRedDotNet.Models.Listings;

namespace OrangeRedDotNet.Models.Links
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
        public Listing<Link> Originals { get; set; }

        /// <summary>
        /// Duplicate Links
        /// </summary>
        public Listing<Link> Duplicates { get; set; }
    }
}
