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
	/// Reddit authentication with a username and password
	/// </summary>
	/// <remarks>
	/// This is not secure, OAuth should be used instead
	/// </remarks>
	public class PasswordAuthentication : IRedditAuthentication, IDisposable
	{
		/// <summary>
		/// HttpClient instance
		/// </summary>
		private static readonly HttpClient _httpClient = new();

		/// <summary>
		/// Builds a request message to send to Reddit
		/// </summary>
		/// <param name="httpMethod">HTTP method to use</param>
		/// <param name="requestUrl">URL for the request</param>
		/// <param name="options"></param>
		/// <returns></returns>
		internal static HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, string requestUrl, PasswordAuthenticationOptions options)
		{
			HttpRequestMessage request = new(httpMethod, new Uri(requestUrl));
			request.Headers.Authorization = new AuthenticationHeaderValue(
				"Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.ClientId}:{options.ClientSecret}"))
			);
			return request;
		}

		/// <summary>
		/// Get a fresh token from Reddit
		/// </summary>
		/// <returns>Auth token</returns>
		internal static async Task<TokenResponse> GetFreshToken(PasswordAuthenticationOptions options)
		{
			HttpRequestMessage request = BuildRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/access_token", options);
			request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{ "grant_type", "password"},
				{ "username", options.Username },
				{ "password", options.Password }
			});

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			string contentString = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TokenResponse>(contentString);
		}

		/// <summary>
		/// Input options
		/// </summary>
		private readonly PasswordAuthenticationOptions _options;

		/// <summary>
		/// The latest token retrieved
		/// </summary>
		private TokenResponse _latestTokenResponse = null;
		/// <summary>
		/// When the latest token expires
		/// </summary>
		private DateTime _latestTokenExpires = DateTime.MinValue;
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
		public async Task<string> GetBearerToken()
		{
			// TODO Need to add proper error handling if token retrieval fails
			if (DateTime.Now >= _latestTokenExpires)
			{
				_latestTokenResponse = await GetFreshToken(_options);
				_latestTokenExpires = DateTime.Now.AddSeconds(_latestTokenResponse.ExpiresIn);
			}
			return _latestTokenResponse.AccessToken;
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
						var request = BuildRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/revoke_token", _options);
						request.Content = new FormUrlEncodedContent(new Dictionary<string, string> 
						{
							{ "token", _latestTokenResponse.AccessToken },
							{ "token_type_hint", "access_token" }
						});
						var response = _httpClient.SendAsync(request).Result;
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
