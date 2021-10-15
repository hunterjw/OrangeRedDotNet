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
		private static readonly HttpClient Client = new();

		/// <summary>
		/// Input options
		/// </summary>
		private readonly PasswordAuthenticationOptions Options;

		/// <summary>
		/// The latest token retrieved
		/// </summary>
		private TokenResponse LatestTokenResponse = null;
		/// <summary>
		/// When the latest token expires
		/// </summary>
		private DateTime LatestTokenExpires = DateTime.MinValue;
		/// <summary>
		/// If this object is disposed or not
		/// </summary>
		private bool Disposed;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options">Password options</param>
		public PasswordAuthentication(PasswordAuthenticationOptions options)
		{
			Options = options;
		}

		/// <summary>
		/// Builds a request message to send to Reddit
		/// </summary>
		/// <param name="httpMethod">HTTP method to use</param>
		/// <param name="requestUrl">URL for the request</param>
		/// <returns></returns>
		private HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, string requestUrl)
		{
			HttpRequestMessage request = new(httpMethod, new Uri(requestUrl));
			request.Headers.Authorization = new AuthenticationHeaderValue(
				"Basic", 
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Options.ClientId}:{Options.ClientSecret}"))
			);
			return request;
		}

		/// <summary>
		/// Get a fresh token from Reddit
		/// </summary>
		/// <returns>Auth token</returns>
		private async Task<TokenResponse> GetFreshToken()
		{
			HttpRequestMessage request = BuildRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/access_token");
			request.Content = new FormUrlEncodedContent(new Dictionary<string, string> 
			{ 
				{ "grant_type", "password"}, 
				{ "username", Options.Username }, 
				{ "password", Options.Password } 
			});

			var response = await Client.SendAsync(request);
			response.EnsureSuccessStatusCode();

			string contentString = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TokenResponse>(contentString);
		}

		public async Task<string> GetBearerToken()
		{
			// TODO Need to add proper error handling if token retrieval fails
			if (DateTime.Now >= LatestTokenExpires)
			{
				LatestTokenResponse = await GetFreshToken();
				LatestTokenExpires = DateTime.Now.AddSeconds(LatestTokenResponse.ExpiresIn);
			}
			return LatestTokenResponse.AccessToken;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					try
					{
						var request = BuildRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/revoke_token");
						request.Content = new FormUrlEncodedContent(new Dictionary<string, string> 
						{
							{ "token", LatestTokenResponse.AccessToken },
							{ "token_type_hint", "access_token" }
						});
						var response = Client.SendAsync(request).Result;
					}
					catch
					{
						// Ignore errors, if the request fails then the token will eventually be cleaned up by the server
					}
				}
				Disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
