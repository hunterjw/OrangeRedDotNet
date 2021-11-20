using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get some random links from Reddit
	/// </summary>
	[Verb("listings-get-random", HelpText = "Get some random links from Reddit")]
	class GetRandomVerb : VerbBase
	{
		/// <summary>
		/// Subreddit to get links from
		/// </summary>
		[Option(HelpText = "Subreddit to get links from")]
		public string Subreddit { get; set; }

		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Listings.GetRandom(Subreddit).ToJson();
		}
	}
}
