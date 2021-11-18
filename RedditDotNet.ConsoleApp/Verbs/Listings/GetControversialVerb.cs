using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Verb to get the most controversial of links
	/// </summary>
	[Verb("listings-get-controversial", HelpText = "Get the most controversial of Links")]
	class GetControversialVerb : SortListingVerb
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Listings.GetControversial(BuildSortListingParameters(), Subreddit));
		}
	}
}
