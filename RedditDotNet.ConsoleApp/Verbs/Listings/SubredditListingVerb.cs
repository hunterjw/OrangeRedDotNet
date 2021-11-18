using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Base class for listings that take a Subreddit parameter
	/// </summary>
	abstract class SubredditListingVerb : ListingVerb
	{
		/// <summary>
		/// Subreddit to get content for
		/// </summary>
		[Option(HelpText = "Subreddit to get content for")]
		public string Subreddit { get; set; }
	}
}
