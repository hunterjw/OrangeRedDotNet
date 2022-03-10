using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
    /// <summary>
    /// Reddit authentication base class
    /// </summary>
    public abstract class AuthenticationBase : IRedditAuthentication
    {
        /// <summary>
        /// HttpClient instance
        /// </summary>
        private static readonly HttpClient _httpClient = new();

        /// <summary>
        /// Build a HttpRequestMessage
        /// </summary>
        /// <param name="httpMethod">Request method</param>
        /// <param name="requestUrl">Request URL</param>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <returns>HttpRequestMessage object</returns>
        private static HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, 
            string requestUrl, string clientId, string clientSecret = null)
        {
            string authString = $"{clientId}";
            if (!string.IsNullOrWhiteSpace(clientSecret))
            {
                authString += $":{clientSecret}";
            }
            HttpRequestMessage request = new(httpMethod, new Uri(requestUrl));
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(authString))
            );
            return request;
        }

        /// <summary>
        /// Get a fresh token from Reddit
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSectet">Client secret</param>
        /// <param name="requestContent">Request content to POST to Reddit</param>
        /// <returns>TokenResponse object</returns>
        internal static async Task<TokenResponse> GetFreshToken(string clientId, 
            string clientSectet = null, Dictionary<string, string> requestContent = null)
        {
            HttpRequestMessage request = BuildRequestMessage(HttpMethod.Post, 
                "https://www.reddit.com/api/v1/access_token", clientId, clientSectet);
            request.Content = new FormUrlEncodedContent(
                requestContent ?? new Dictionary<string, string>());

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(contentString);
        }

        /// <summary>
        /// Revoke a token
        /// </summary>
        /// <param name="token">Token value</param>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client secret</param>
        /// <param name="tokenType">Token type</param>
        /// <returns>Awaitable task</returns>
        internal static async Task RevokeToken(string token, string clientId, 
            string clientSecret = null, string tokenType = null)
        {
            var request = BuildRequestMessage(HttpMethod.Post, 
                "https://www.reddit.com/api/v1/revoke_token", clientId, clientSecret);
            Dictionary<string, string> requestContent = new()
            {
                { "token", token },
            };
            if (!string.IsNullOrWhiteSpace(tokenType))
            {
                requestContent.Add("token_type_hint", tokenType);
            }
            request.Content = new FormUrlEncodedContent(requestContent);
            _ = await _httpClient.SendAsync(request);
        }

        /// <summary>
        /// Function to load cached auth
        /// </summary>
        private readonly Func<TokenResponse> _load;
        /// <summary>
        /// Action to save auth
        /// </summary>
        private readonly Action<TokenResponse> _save;

        /// <summary>
        /// When the latest token expires
        /// </summary>
        private DateTime _latestTokenExpires = DateTime.MinValue;

        /// <summary>
        /// The latest token retrieved
        /// </summary>
        protected TokenResponse _latestTokenResponse = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="load">Function to load cached auth</param>
        /// <param name="save">Action to save auth</param>
        public AuthenticationBase(Func<TokenResponse> load = null, Action<TokenResponse> save = null)
        {
            _load = load;
            _save = save;
        }

        /// <summary>
        /// Get a fresh token from Reddit
        /// </summary>
        /// <returns>Auth token</returns>
        public abstract Task<TokenResponse> GetFreshToken();

        /// <inheritdoc/>
        public abstract Task RevokeToken();

        /// <inheritdoc/>
        public async Task<string> GetBearerToken()
        {
            // TODO Need to add proper error handling if token retrieval fails
            if (DateTime.Now >= _latestTokenExpires)
            {
                if (_load != null)
                {
                    TokenResponse loadedToken = _load();
                    if (_latestTokenResponse == null || 
                        _latestTokenResponse.Expires < loadedToken?.Expires)
                    {
                        _latestTokenResponse = loadedToken;
                    }
                }
                if (_latestTokenResponse == null || 
                    DateTime.Now >= _latestTokenResponse.Expires)
                {
                    _latestTokenResponse = await GetFreshToken();
                    _latestTokenResponse.Retrieved = DateTime.Now;
                    _latestTokenResponse.Expires = _latestTokenResponse.Retrieved
                        .AddSeconds(_latestTokenResponse.ExpiresIn);
                    _save?.Invoke(_latestTokenResponse);
                }
                _latestTokenExpires = _latestTokenResponse.Expires;
            }
            return _latestTokenResponse.AccessToken;
        }
    }
}
