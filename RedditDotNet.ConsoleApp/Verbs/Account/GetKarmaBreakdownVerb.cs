using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get a breakdown of karma by subreddit for the current user
	/// </summary>
	[Verb("account-get-karmabreakdown", 
		HelpText = "Get a breakdown of karma by subreddit for the current user")]
	internal class GetKarmaBreakdownVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetKarmaBreakdown().Result.ToJson();
		}
	}
}
