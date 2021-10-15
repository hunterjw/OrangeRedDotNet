using CommandLine;

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
			return ToJson(reddit.Account.GetTrusted().Result);
		}
	}
}
