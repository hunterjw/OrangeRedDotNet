using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get list of trusted users for the current user
	/// </summary>
	[Verb("account-get-trusted", HelpText = "Get list of trusted users for the current user")]
	internal class GetTrustedVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetTrusted()).ToJson();
		}
	}
}
