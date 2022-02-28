using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
    /// <summary>
    /// Reddit authentication with a username and password
    /// </summary>
    /// <remarks>
    /// This is not secure, OAuth should be used instead
    /// </remarks>
    public class PasswordAuthentication : AuthenticationBase, IDisposable
	{
		/// <summary>
		/// Get a fresh token from Reddit
		/// </summary>
		/// <param name="options">Password authentication options</param>
		/// <returns>Auth token</returns>
		internal static async Task<TokenResponse> GetFreshToken(PasswordAuthenticationOptions options)
		{
            Dictionary<string, string> requestContent = new()
            {
				{ "grant_type", "password"},
				{ "username", options.Username },
				{ "password", options.Password }
			};
			return await GetFreshToken(options.ClientId, options.ClientSecret, requestContent);
		}

		/// <summary>
		/// Input options
		/// </summary>
		private readonly PasswordAuthenticationOptions _options;

		/// <summary>
		/// If this object is disposed or not
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options">Password options</param>
		public PasswordAuthentication(PasswordAuthenticationOptions options)
		{
			_options = options;
		}

		/// <inheritdoc/>
        public override async Task<TokenResponse> GetFreshToken()
        {
			return await GetFreshToken(_options);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					try
					{
						RevokeToken(_latestTokenResponse.AccessToken, 
								_options.ClientId, 
								_options.ClientSecret, 
								"access_token")
							.Wait();
					}
					catch
					{
						// Ignore errors, if the request fails then the token will eventually be cleaned up by the server
					}
				}
				_disposed = true;
			}
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
