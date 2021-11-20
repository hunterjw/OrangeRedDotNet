using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get the newest links on Reddit
	/// </summary>
	[Verb("listings-get-new", HelpText = "Get the newest links on Reddit")]
	class GetNewVerb : SubredditListingVerb
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Listings.GetNew(BuildListingParameters(), Subreddit).Result.ToJson();
		}
	}
}
