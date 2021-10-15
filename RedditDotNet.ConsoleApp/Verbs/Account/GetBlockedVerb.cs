using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get blocked users
	/// </summary>
	[Verb("get-blocked")]
	class GetBlockedVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Account.GetBlocked().Result);
		}
	}
}
