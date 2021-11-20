using CommandLine;
using RedditDotNet.Extensions;

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
			return reddit.Listings.GetRising(BuildListingParameters(), Subreddit).ToJson();
		}
	}
}
