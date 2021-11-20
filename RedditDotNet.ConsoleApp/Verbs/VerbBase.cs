using Newtonsoft.Json;
using RedditDotNet.Authentication;

namespace RedditDotNet.ConsoleApp.Verbs
{
	/// <summary>
	/// Base class for command line Reddit verbs
	/// </summary>
	internal abstract class VerbBase
	{
		/// <summary>
		/// Run this verb
		/// </summary>
		/// <returns>Output for the standard output stream</returns>
		public string Run()
		{
			using var auth = new PasswordAuthentication(AppSettings.PasswordAuthenticationOptions);
			var reddit = new Reddit("C# Test 1.0.0", auth); // TODO need to generate proper user agent string here
			return Run(reddit);
		}

		/// <summary>
		/// Run this verb
		/// </summary>
		/// <param name="reddit">Reddit instance to use to run the verb</param>
		/// <returns>Output for the standard output stream</returns>
		public abstract string Run(Reddit reddit);
	}
}
