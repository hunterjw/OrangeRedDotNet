using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Base class for sort listing verbs
	/// </summary>
	abstract class SortListingVerb : SubredditListingVerb
	{
		/// <summary>
		/// Timescale sort for the returned Links
		/// </summary>
		[Option(Required = true, HelpText = "Timescale sort for the returned Links")]
		public string Timescale { get; set; }

		/// <summary>
		/// Build a sort listing parameters object
		/// </summary>
		/// <returns>Sort listing parameters object</returns>
		protected SortListingParameters BuildSortListingParameters()
		{
			return new SortListingParameters
			{
				After = After,
				Before = Before,
				Count = Count,
				Limit = Limit,
				ShowAll = ShowAll,
				ExpandSubreddits = ExpandSubreddits,
				Timescale = Timescale.ToEnumFromDescriptionString<Timescale>()
			};
		}
	}
}
