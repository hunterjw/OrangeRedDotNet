using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get rising Links on Reddit
	/// </summary>
	[Verb("listings-get-rising", HelpText = "Get rising Links on Reddit")]
	class GetRisingVerb : SubredditListingVerb
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Listings.GetRising(BuildListingParameters(), Subreddit));
		}
	}
}
