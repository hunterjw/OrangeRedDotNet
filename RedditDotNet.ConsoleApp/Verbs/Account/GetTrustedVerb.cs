using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get trusted users
	/// </summary>
	[Verb("get-trusted")]
	class GetTrustedVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetTrusted().Result.ToJson();
		}
	}
}
