using CommandLine;

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
			return ToJson(reddit.Account.GetKarmaBreakdown().Result);
		}
	}
}
