using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get the hottest Links on Reddit
	/// </summary>
	[Verb("listings-get-hot", HelpText = "Get the hottest Links on Reddit")]
	class GetHotVerb : SubredditListingVerb
	{
		/// <summary>
		/// The location to get the hottest Links for
		/// </summary>
		[Option(Required = true, HelpText = "The location to get the hottest Links for")]
		public string Location { get; set; }

		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Listings.GetHot(BuildLocationListingParameters(), Subreddit).Result);
		}

		/// <summary>
		/// Builds a location listing parameters object
		/// </summary>
		/// <returns>Location listing parameters object</returns>
		protected LocationListingParameters BuildLocationListingParameters()
		{
			return new LocationListingParameters
			{
				After = After,
				Before = Before,
				Count = Count,
				Limit = Limit,
				ShowAll = ShowAll,
				ExpandSubreddits = ExpandSubreddits,
				Location = Location.ToEnumFromDescriptionString<Location>()
			};
		}
	}
}
