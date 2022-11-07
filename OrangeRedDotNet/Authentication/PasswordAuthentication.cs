using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Authentication
{
    /// <summary>
    /// Reddit authentication with a username and password
    /// </summary>
    /// <remarks>
    /// This is not secure, OAuth should be used instead
    /// </remarks>
    public class PasswordAuthentication : AuthenticationBase
    {
        /// <summary>
        /// Input options
        /// </summary>
        private readonly PasswordAuthenticationOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Password options</param>
        public PasswordAuthentication(PasswordAuthenticationOptions options, 
            Func<Task<TokenResponse>> load = null, 
            Func<TokenResponse, Task> save = null)
            : base(load, save)
        {
            _options = options;
        }

        /// <inheritdoc/>
        public override async Task<TokenResponse> GetFreshToken()
        {
            Dictionary<string, string> requestContent = new()
            {
                { "grant_type", "password" },
                { "username", _options.Username },
                { "password", _options.Password }
            };
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
