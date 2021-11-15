using RedditDotNet.Extensions;
using System.Collections.Generic;

namespace RedditDotNet.Models.Listings
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
		public new IDictionary<string, string> ToQueryParameters()
		{
			var dict = base.ToQueryParameters();
			dict.Add("g", Location.ToDescriptionString());
			return dict;
		}
	}
}
