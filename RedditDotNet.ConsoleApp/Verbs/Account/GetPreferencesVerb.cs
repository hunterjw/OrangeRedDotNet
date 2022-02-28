using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get the user preferences for the current user
	/// </summary>
	[Verb("account-get-preferences", 
		HelpText = "Get the user preferences for the current user")]
	internal class GetPreferencesVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetPreferences()).ToJson();
		}
	}
}
