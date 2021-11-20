using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Listings
{
	/// <summary>
	/// Listing data
	/// </summary>
	/// <typeparam name="T">Type of the data in the listing</typeparam>
	public class ListingData<T>
	{
		[JsonProperty("after")]
		public string After { get; set; }

		[JsonProperty("before")]
		public string Before { get; set; }

		[JsonProperty("children")]
		public List<T> Children { get; set; }

		[JsonProperty("dist")]
		public int? Count { get; set; }

		[JsonProperty("geo_filter")]
		public object GeoFilter { get; set; }

		[JsonProperty("modhash")]
		public object ModHash { get; set; }
	}
}
