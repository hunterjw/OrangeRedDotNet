using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get current user identity
	/// </summary>
	[Verb("get-identity")]
	class GetIdentityVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetIdentity().Result.ToJson();
		}
	}
}
