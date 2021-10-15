using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Verb to get preferences
	/// </summary>
	[Verb("get-preferences")]
	class GetPreferencesVerb : VerbBase
	{
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Account.GetPreferences().Result);
		}
	}
}
