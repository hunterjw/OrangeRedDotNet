using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get a breakdown of karma by subreddit for the current user
	/// </summary>
	[Verb("account-get-karmabreakdown", 
		HelpText = "Get a breakdown of karma by subreddit for the current user")]
	internal class GetKarmaBreakdownVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetKarmaBreakdown()).ToJson();
		}
	}
}
