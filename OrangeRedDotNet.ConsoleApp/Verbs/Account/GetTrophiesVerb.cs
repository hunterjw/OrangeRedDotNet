using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get awards for the current user
	/// </summary>
	[Verb("account-get-trophies", HelpText = "Get awards for the current user")]
	internal class GetTrophiesVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetTrophies()).ToJson();
		}
	}
}
