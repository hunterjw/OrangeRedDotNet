using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get the identity of the current user
	/// </summary>
	[Verb("account-get-identity", HelpText = "Get the identity of the current user")]
	internal class GetIdentityVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetIdentity().Result.ToJson();
		}
	}
}
