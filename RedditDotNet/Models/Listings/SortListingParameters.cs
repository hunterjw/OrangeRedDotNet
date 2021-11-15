﻿using RedditDotNet.Extensions;
using System.Collections.Generic;

namespace RedditDotNet.Models.Listings
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
        public new IDictionary<string, string> ToQueryParameters()
		{
			IDictionary<string, string> dict = base.ToQueryParameters();
			dict.Add("t", Timescale.ToDescriptionString());
			return dict;
		}
	}
}