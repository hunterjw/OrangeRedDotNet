using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Listings
{
    /// <summary>
    /// Listing parameters with an assosiated location
    /// </summary>
    public class LocationListingParameters : ListingParameters
    {
        /// <summary>
        /// Location to get listing for
        /// </summary>
        public Location Location { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            var dict = base.ToQueryParameters();
            dict.Add("g", Location.ToDescriptionString());
            return dict;
        }

        /// <inheritdoc/>
        public override ListingParameters Copy()
        {
            return new LocationListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                ShowAll = ShowAll,
                Location = Location,
            };
        }
    }
}
