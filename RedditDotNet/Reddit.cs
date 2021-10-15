using RedditDotNet.Authentication;
using RedditDotNet.Controllers;

namespace RedditDotNet
{
	/// <summary>
	/// Reddit, the frontpage of the internet
	/// </summary>
	public class Reddit
	{
		/// <summary>
		/// Input user agent string
		/// </summary>
		private readonly string UserAgent;
		/// <summary>
		/// Input IRedditAuthentication instance
		/// </summary>
		private readonly IRedditAuthentication RedditAuthentication;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="userAgent">User agent string</param>
		/// <param name="redditAuthenticaiton">Authentication to use to connect to Reddit</param>
		public Reddit(string userAgent, IRedditAuthentication redditAuthenticaiton)
		{
			UserAgent = userAgent;
			RedditAuthentication = redditAuthenticaiton;
		}

		/// <summary>
		/// Account operations
		/// </summary>
		public AccountController Account
		{
			get
			{
				if (AccountController == null)
				{
					AccountController = new AccountController(UserAgent, RedditAuthentication);
				}
				return AccountController;
			}
		}
		private AccountController AccountController = null;
	}
}
