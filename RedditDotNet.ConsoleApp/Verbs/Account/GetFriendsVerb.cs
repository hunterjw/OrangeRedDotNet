using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get friends
	/// </summary>
	[Verb("get-friends")]
	class GetFriendsVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Account.GetFriends().Result);
		}
	}
}
