using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp
{
	/// <summary>
	/// Service for storing and getting the current users identity
	/// </summary>
    public class IdentityService
    {
		/// <summary>
		/// Reddit instance
		/// </summary>
		private readonly Reddit _reddit;
		/// <summary>
		/// Current identity
		/// </summary>
		private AccountData _identity;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reddit">Reddit instance</param>
		public IdentityService(Reddit reddit)
		{
			_reddit = reddit;
		}

		/// <summary>
		/// Get the current users identity
		/// </summary>
		/// <param name="forceReload">To force reload from Reddit or not</param>
		/// <returns>Identity information</returns>
		public async Task<AccountData> GetIdentity(bool forceReload = false)
        {
			if (_identity == null || forceReload)
            {
				_identity = await _reddit.Account.GetIdentity();
            }
			return _identity;
        }
    }
}
