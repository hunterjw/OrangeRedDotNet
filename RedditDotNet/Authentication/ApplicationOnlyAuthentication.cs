using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
    /// <summary>
    /// Application only authentication for Reddit
    /// Authentication for when a user is not logged in
    /// </summary>
    public class ApplicationOnlyAuthentication : AuthenticationBase
    {
        /// <summary>
        /// Input options
        /// </summary>
        private readonly ApplicationOnlyAuthenticationOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options object</param>
		/// <param name="load">Function to load cached auth</param>
		/// <param name="save">Action to save auth</param>
        public ApplicationOnlyAuthentication(ApplicationOnlyAuthenticationOptions options, 
            Func<TokenResponse> load = null, Action<TokenResponse> save = null)
            : base(load, save)
        {
            _options = options;
        }

        /// <inheritdoc/>
        public override async Task<TokenResponse> GetFreshToken()
        {
            string grantString = string.Empty;
            if (_options.GrantType == ApplicationOnlyGrantType.InstalledCLient)
            {
                grantString = "https://oauth.reddit.com/grants/installed_client";
            }
            else if (_options.GrantType == ApplicationOnlyGrantType.ClientCredentials)
            {
                grantString = "client_credentials";
            }

            Dictionary<string, string> requestContent = new()
            {
                { "grant_type", grantString },
            };
            if (_options.GrantType == ApplicationOnlyGrantType.InstalledCLient)
            {
                requestContent.Add("device_id", _options.DoNotTrack ? "DO_NOT_TRACK_THIS_DEVICE" : _options.DeviceId);
            }

            return await GetFreshToken(_options.ClientId, _options.ClientSecret, requestContent);
        }

        /// <inheritdoc/>
        public override async Task RevokeToken()
        {
            await RevokeToken(_latestTokenResponse.AccessToken,
                    _options.ClientId,
                    _options.ClientSecret,
                    "access_token");
        }
    }
}
