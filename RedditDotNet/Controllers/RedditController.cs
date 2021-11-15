using Newtonsoft.Json;
using RedditDotNet.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RedditDotNet.Controllers
{
	/// <summary>
	/// Base class for controllers for Reddit
	/// </summary>
	public abstract class RedditController
	{
		/// <summary>
		/// Base URL to make requests to
		/// </summary>
		private static readonly Uri BaseUri = new("https://oauth.reddit.com");
		/// <summary>
		/// Shared HttpClient instance
		/// </summary>
		private static readonly HttpClient HttpClient = new();

		/// <summary>
		/// Input user agent string
		/// </summary>
		private readonly string UserAgent;
		/// <summary>
		/// Input reddit authentication
		/// </summary>
		private readonly IRedditAuthentication RedditAuthentication;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="userAgent">User agent string</param>
		/// <param name="redditAuthentication">Reddit authentication to use</param>
		public RedditController(string userAgent, IRedditAuthentication redditAuthentication)
		{
			UserAgent = userAgent;
			RedditAuthentication = redditAuthentication;
		}

		/// <summary>
		/// Deserialize the content of a HttpResponseMessage to <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Type to deserialize to</typeparam>
		/// <param name="responseMessage">Response from a HTTP operation</param>
		/// <returns><typeparamref name="T"/> instance</returns>
		internal static async Task<T> DeserializeToObject<T>(HttpResponseMessage responseMessage)
		{
			var serializer = new JsonSerializer();
			using var streamReader = new StreamReader(await responseMessage.Content.ReadAsStreamAsync());
			using var jsonTextReader = new JsonTextReader(streamReader);
			return serializer.Deserialize<T>(jsonTextReader);
		}

		/// <summary>
		/// HTTP Get
		/// </summary>
		/// <param name="relativeUrl">URL to make the request to</param>
		/// <returns>HttpResponseMessage</returns>
		internal async Task<HttpResponseMessage> Get(string relativeUrl)
		{
			return await Get(relativeUrl, null);
		}

		/// <summary>
		/// HTTP Get
		/// </summary>
		/// <param name="relativeUrl">URL to make the request to</param>
		/// <param name="queryParameters">Query parameters for the request</param>
		/// <returns>HttpResponseMessage</returns>
		internal async Task<HttpResponseMessage> Get(string relativeUrl, IDictionary<string, string> queryParameters)
		{
			UriBuilder builder = new(new Uri(BaseUri, relativeUrl));
			if (queryParameters != null && queryParameters.Count > 0)
			{
				builder.Query = await new FormUrlEncodedContent(queryParameters).ReadAsStringAsync();
			}
			HttpRequestMessage request = new(HttpMethod.Get, builder.ToString());
			var token = await RedditAuthentication.GetBearerToken();
			request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
			if (!string.IsNullOrWhiteSpace(UserAgent))
			{
				request.Headers.UserAgent.Add(new ProductInfoHeaderValue("CSharpRedditTest", "1.0.0")); // TODO this needs to be more robust/configurable
			}

			HttpResponseMessage response = await HttpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return response;
		}

		/// <summary>
		/// HTTP Get with the response deserialzed to <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Type to deserialze the HttpResponseMessage to</typeparam>
		/// <param name="relativeUrl">URL to make the request to</param>
		/// <returns><typeparamref name="T"/> instance</returns>
		internal async Task<T> Get<T>(string relativeUrl)
		{
			return await DeserializeToObject<T>(await Get(relativeUrl));
		}

		/// <summary>
		/// HTTP Get with the response deserialzed to <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Type to deserialze the HttpResponseMessage to</typeparam>
		/// <param name="relativeUrl">URL to make the request to</param>
		/// <param name="queryParameters">Query parameters for the request</param>
		/// <returns><typeparamref name="T"/> instance</returns>
		internal async Task<T> Get<T>(string relativeUrl, IDictionary<string, string> queryParameters)
		{
			return await DeserializeToObject<T>(await Get(relativeUrl, queryParameters));
		}
	}
}
