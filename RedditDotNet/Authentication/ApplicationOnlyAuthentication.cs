using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
    /// <summary>
    /// Application only authentication for Reddit
    /// Authentication for when a user is not logged in
    /// </summary>
    public class ApplicationOnlyAuthentication : AuthenticationBase, IDisposable
    {
        /// <summary>
        /// Get a fresh token from Reddit
        /// </summary>
        /// <param name="options">Application only authentication options</param>
        /// <returns>Auth token</returns>
        internal static async Task<TokenResponse> GetFreshToken(ApplicationOnlyAuthenticationOptions options)
        {
            string grantString = string.Empty;
            if (options.GrantType == ApplicationOnlyGrantType.InstalledCLient)
            {
                grantString = "https://oauth.reddit.com/grants/installed_client";
            }
            else if (options.GrantType == ApplicationOnlyGrantType.ClientCredentials)
            {
                grantString = "client_credentials";
            }

            Dictionary<string, string> requestContent = new()
            {
                { "grant_type", grantString },
            };
            if (options.GrantType == ApplicationOnlyGrantType.InstalledCLient)
            {
                requestContent.Add("device_id", options.DoNotTrack ? "DO_NOT_TRACK_THIS_DEVICE" : options.DeviceId);
            }

            return await GetFreshToken(options.ClientId, options.ClientSecret, requestContent);
        }

        /// <summary>
        /// Input options
        /// </summary>
        private readonly ApplicationOnlyAuthenticationOptions _options;

        /// <summary>
        /// If the object is disposed or not
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options object</param>
        public ApplicationOnlyAuthentication(ApplicationOnlyAuthenticationOptions options)
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
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
