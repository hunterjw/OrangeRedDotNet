using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get list of trusted users for the current user
	/// </summary>
	[Verb("account-get-trusted", HelpText = "Get list of trusted users for the current user")]
	internal class GetTrustedVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetTrusted().Result.ToJson();
		}
	}
}
