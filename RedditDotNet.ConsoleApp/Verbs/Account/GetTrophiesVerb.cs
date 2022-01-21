using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get awards for the current user
	/// </summary>
	[Verb("account-get-trophies", HelpText = "Get awards for the current user")]
	internal class GetTrophiesVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetTrophies().Result.ToJson();
		}
	}
}
