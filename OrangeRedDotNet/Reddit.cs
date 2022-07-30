using OrangeRedDotNet.Authentication;
using OrangeRedDotNet.Controllers;

namespace OrangeRedDotNet
{
    /// <summary>
    /// Reddit, the frontpage of the internet
    /// </summary>
    public class Reddit
	{
		/// <summary>
		/// Input IRedditAuthentication instance
		/// </summary>
		private readonly IRedditAuthentication RedditAuthentication;
		/// <summary>
		/// Input RedditUserAgent
		/// </summary>
		private readonly RedditUserAgent RedditUserAgent;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="redditAuthentication">Authentication to use to connect to Reddit</param>
		/// <param name="redditUserAgent">
		///		Reddit user agent.
		///		If the reddit client is being used within a web application hosted in a browser 
		///		(i.e. Blazor Webassembly), do not provide a user agent as the browsers user agent
		///		will be used instead.
		///	</param>
		public Reddit(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
		{
			RedditAuthentication = redditAuthentication;
			RedditUserAgent = redditUserAgent;
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
					AccountController = new AccountController(RedditAuthentication, RedditUserAgent);
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
					ListingsController = new ListingsController(RedditAuthentication, RedditUserAgent);
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
					MultiController = new MultiController(RedditAuthentication, RedditUserAgent);
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
					UsersController = new UsersController(RedditAuthentication, RedditUserAgent);
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
					LinksAndCommentsController = new LinksAndCommentsController(RedditAuthentication, RedditUserAgent);
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
					SubredditsController = new SubredditsController(RedditAuthentication, RedditUserAgent);
                }
				return SubredditsController;
            }
        }
		private SubredditsController SubredditsController = null;

		/// <summary>
		/// Search operations
		/// </summary>
		public SearchController Search
        {
			get
            {
				if (SearchController == null)
                {
					SearchController = new SearchController(RedditAuthentication, RedditUserAgent);
                }
				return SearchController;
            }
        }
		private SearchController SearchController = null;
	}
}
