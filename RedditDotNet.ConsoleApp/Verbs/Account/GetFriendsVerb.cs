using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get the list of friends for the current user
	/// </summary>
	[Verb("account-get-friends", HelpText = "Get the list of friends for the current user")]
	internal class GetFriendsVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetFriends()).ToJson();
		}
	}
}
