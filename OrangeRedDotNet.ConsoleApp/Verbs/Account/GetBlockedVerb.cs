using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get list of blocked users for the current user
	/// </summary>
	[Verb("account-get-blocked", HelpText = "Get list of blocked users for the current user")]
	internal class GetBlockedVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetBlocked()).ToJson();
		}
	}
}
