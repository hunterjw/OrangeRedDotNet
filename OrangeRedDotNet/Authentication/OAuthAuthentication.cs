using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Authentication
{
    /// <summary>
    /// OAuth authentication for Reddit
    /// See https://github.com/reddit-archive/reddit/wiki/OAuth2
    /// </summary>
    public class OAuthAuthentication : AuthenticationBase
    {
        /// <summary>
        /// Input options
        /// </summary>
        private readonly OAuthAuthenticationOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options object</param>
        /// <param name="load">Function to load a previously saved token response</param>
        /// <param name="save">Function to save token responses</param>
        public OAuthAuthentication(OAuthAuthenticationOptions options,
            Func<Task<TokenResponse>> load = null,
            Func<TokenResponse, Task> save = null)
            : base(load, save)
        {
            _options = options;
        }

        /// <summary>
        /// Get an authorization URL to direct users to
        /// </summary>
        /// <param name="state">Application state</param>
        /// <returns>Authorization URL</returns>
        public async Task<string> GetAuthorizationUrl(string state)
        {
            var queryString = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", _options.ClientId },
                { "response_type", "code" },
                { "state", state },
                { "redirect_uri", _options.RedirectUrl },
                { "duration", _options.Duration switch {
                    OAuthDuration.Temporary => "temporary",
                    OAuthDuration.Permanent => "permanent",
                    _ => string.Empty } },
                { "scope", _options.Scopes?.Count > 0 ? string.Join(' ', _options.Scopes) : "*" },
            });
            UriBuilder uriBuilder = new(new Uri($"https://www.reddit.com/api/v1/authorize{(_options.Compact ? ".compact" : "")}"))
            {
                Query = await queryString.ReadAsStringAsync()
            };
            return uriBuilder.ToString();
        }

        /// <summary>
        /// Parse the results from the apps redirect URL
        /// </summary>
        /// <param name="code">One time code</param>
        /// <param name="state">Application state</param>
        /// <param name="expectedState">Expected application state</param>
        /// <param name="error">Error (if errors encountered)</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="Exception">Thrown if error from Reddit or state does not match</exception>
        public async Task ParseRedirectUrl(string code, string state, string expectedState, string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                throw new Exception($"Error with OAuth authorization: {error}");
            }
            if (!state.Equals(expectedState))
            {
                throw new Exception($"OAuth state mismatch.{Environment.NewLine}" +
                    $"Expected: {expectedState}{Environment.NewLine}" +
                    $"Actual: {state}");
            }

            Dictionary<string, string> content = new()
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", _options.RedirectUrl }
            };
            _latestTokenResponse = await GetFreshToken(_options.ClientId, requestContent: content);
            _save?.Invoke(_latestTokenResponse);
        }

        /// <inheritdoc/>
        public override async Task<TokenResponse> GetFreshToken()
        {
            if (_options.Duration != OAuthDuration.Permanent)
            {
                throw new Exception("Cannot refresh non-permanent tokens");
            }
            if (string.IsNullOrWhiteSpace(_latestTokenResponse?.RefreshToken))
            {
                throw new Exception("Cannot refresh without a refresh token");
            }

            string refreshToken = _latestTokenResponse.RefreshToken;
            Dictionary<string, string> content = new()
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };
            var response = await GetFreshToken(_options.ClientId, requestContent: content);
            response.RefreshToken = refreshToken; // The refresh token is not included in the refresh
            return response;
        }

        /// <inheritdoc/>
        public override async Task RevokeToken()
        {
            await RevokeToken(_latestTokenResponse.AccessToken,
                    _options.ClientId,
                    tokenType: "access_token");
            if (!string.IsNullOrWhiteSpace(_latestTokenResponse?.RefreshToken))
            {
                await RevokeToken(_latestTokenResponse.RefreshToken,
                    _options.ClientId,
                    tokenType: "refresh_token");
            }
        }
    }
}
