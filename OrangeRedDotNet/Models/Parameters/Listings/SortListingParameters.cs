using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Listings
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

        /// <inheritdoc/>
        public override ListingParameters Copy()
        {
            return new SortListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                ShowAll = ShowAll,
                Timescale = Timescale,
            };
        }
    }
}
