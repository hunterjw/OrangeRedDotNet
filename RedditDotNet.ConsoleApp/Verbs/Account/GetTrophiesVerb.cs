using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get trophies
	/// </summary>
	[Verb("get-trophies")]
	class GetTrophiesVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Account.GetTrophies().Result);
		}
	}
}
