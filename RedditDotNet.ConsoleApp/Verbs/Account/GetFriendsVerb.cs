using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get the list of friends for the current user
	/// </summary>
	[Verb("account-get-friends", HelpText = "Get the list of friends for the current user")]
	internal class GetFriendsVerb : VerbBase
	{
		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return reddit.Account.GetFriends().Result.ToJson();
		}
	}
}
