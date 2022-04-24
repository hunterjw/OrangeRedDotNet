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

		/// <summary>
		/// Listings operations
		/// </summary>
		public ListingsController Listings
		{
			get
			{
				if (ListingsController == null)
				{
					ListingsController = new ListingsController(UserAgent, RedditAuthentication);
				}
				return ListingsController;
			}
		}
		private ListingsController ListingsController = null;

		/// <summary>
		/// MultiReddit operations
		/// </summary>
		public MultiController Multis
        {
			get
            {
				if (MultiController == null)
                {
					MultiController = new MultiController(UserAgent, RedditAuthentication);
                }
				return MultiController;
            }
        }
		private MultiController MultiController = null;

		/// <summary>
		/// User operations
		/// </summary>
		public UsersController Users
        {
            get
            {
				if (UsersController == null)
                {
					UsersController = new UsersController(UserAgent, RedditAuthentication);
                }
				return UsersController;
            }
        }
		private UsersController UsersController = null;

		/// <summary>
		/// Link and Comment operations
		/// </summary>
		public LinksAndCommentsController LinksAndComments
        {
            get
            {
				if (LinksAndCommentsController == null)
                {
					LinksAndCommentsController = new LinksAndCommentsController(UserAgent, RedditAuthentication);
                }
				return LinksAndCommentsController;
            }
        }
		private LinksAndCommentsController LinksAndCommentsController = null;

		/// <summary>
		/// Subreddit operations
		/// </summary>
		public SubredditsController Subreddits
        {
			get
            {
				if (SubredditsController == null)
                {
					SubredditsController = new SubredditsController(UserAgent, RedditAuthentication);
                }
				return SubredditsController;
            }
        }
		private SubredditsController SubredditsController = null;
	}
}
