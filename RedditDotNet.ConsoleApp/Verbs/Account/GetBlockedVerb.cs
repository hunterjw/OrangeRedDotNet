using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get list of blocked users for the current user
	/// </summary>
	[Verb("account-get-blocked", HelpText = "Get list of blocked users for the current user")]
	internal class GetBlockedVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetBlocked().Result.ToJson();
		}
	}
}
