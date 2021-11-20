using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get karma breakdown by subreddit
	/// </summary>
	[Verb("get-karmabreakdown")]
	class GetKarmaBreakdownVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetKarmaBreakdown().Result.ToJson();
		}
	}
}
