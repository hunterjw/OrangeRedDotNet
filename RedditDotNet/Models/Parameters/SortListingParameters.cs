using RedditDotNet.Extensions;
using System.Collections.Generic;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Listing parameters with an assosiated timescale sort
    /// </summary>
    public class SortListingParameters : ListingParameters
    {
        /// <summary>
        /// Timescale to sort on
        /// </summary>
        public Timescale Timescale { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            IDictionary<string, string> dict = base.ToQueryParameters();
            dict.Add("t", Timescale.ToDescriptionString());
            return dict;
        }
    }
}
