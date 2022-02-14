using System;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
	/// <summary>
	/// Authentication that has the result cached for use between program runs
	/// </summary>
    public abstract class CachedAuthentication : IRedditAuthentication
    {
		/// <summary>
		/// Function to load cached auth
		/// </summary>
        private readonly Func<TokenResponse> _load;
		/// <summary>
		/// Action to save auth
		/// </summary>
        private readonly Action<TokenResponse> _save;

		/// <summary>
		/// The latest token retrieved
		/// </summary>
		private TokenResponse LatestTokenResponse = null;
		/// <summary>
		/// When the latest token expires
		/// </summary>
		private DateTime LatestTokenExpires = DateTime.MinValue;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="load">Function to load cached auth</param>
		/// <param name="save">Action to save auth</param>
		public CachedAuthentication(Func<TokenResponse> load, Action<TokenResponse> save)
        {
            _load = load;
            _save = save;
		}

		/// <summary>
		/// Get a fresh token from Reddit
		/// </summary>
		/// <returns>Auth token</returns>
		protected abstract Task<TokenResponse> GetFreshToken();

		/// <inheritdoc/>
		public async Task<string> GetBearerToken()
		{
			// TODO Need to add proper error handling if token retrieval fails
			if (DateTime.Now >= LatestTokenExpires)
			{
				if (_load != null)
                {
					LatestTokenResponse = _load();
                }
				if (LatestTokenResponse == null || DateTime.Now >= LatestTokenResponse.Expires)
				{
					LatestTokenResponse = await GetFreshToken();
					LatestTokenResponse.Retrieved = DateTime.Now;
					LatestTokenResponse.Expires = LatestTokenResponse.Retrieved.AddSeconds(LatestTokenResponse.ExpiresIn);
					_save?.Invoke(LatestTokenResponse);
				}
				LatestTokenExpires = LatestTokenResponse.Expires;
            }
			return LatestTokenResponse.AccessToken;
		}
	}
}
