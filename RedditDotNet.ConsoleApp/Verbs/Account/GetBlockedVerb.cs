using CommandLine;
using RedditDotNet.Extensions;

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
			return reddit.Account.GetBlocked().Result.ToJson();
		}
	}
}
