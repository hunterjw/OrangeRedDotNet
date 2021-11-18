using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get the top Links on Reddit
	/// </summary>
	[Verb("listings-get-top", HelpText = "Get the top Links on Reddit")]
	class GetTopVerb : SortListingVerb
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Listings.GetTop(BuildSortListingParameters(), Subreddit).Result);
		}
	}
}
