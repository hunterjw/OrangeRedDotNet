using Newtonsoft.Json;
using RedditDotNet.Authentication;
using RedditDotNet.Exceptions;
using RedditDotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// Input reddit authentication
        /// </summary>
        private readonly IRedditAuthentication RedditAuthentication;
        /// <summary>
        /// Input RedditUserAgent
        /// </summary>
        private readonly RedditUserAgent RedditUserAgent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="redditAuthentication">Reddit authentication to use</param>
		/// <param name="redditUserAgent">
		///		Reddit user agent.
		///		If the reddit client is being used within a web application hosted in a browser 
		///		(i.e. Blazor Webassembly), do not provide a user agent as the browsers user agent
		///		will be used instead.
		///	</param>
        public RedditController(IRedditAuthentication redditAuthentication, RedditUserAgent redditUserAgent = null)
        {
            RedditAuthentication = redditAuthentication;
            RedditUserAgent = redditUserAgent;
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

        #region Get
        /// <summary>
        /// HTTP Get
        /// </summary>
        /// <param name="relativeUrl">URL to make the request to</param>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
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
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
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
            if (RedditUserAgent != null)
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(RedditUserAgent.Name, RedditUserAgent.Version));
            }

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RedditApiException(response);
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return response;
        }

        /// <summary>
        /// HTTP Get with the response deserialzed to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialze the HttpResponseMessage to</typeparam>
        /// <param name="relativeUrl">URL to make the request to</param>
        /// <returns><typeparamref name="T"/> instance</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
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
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<T> Get<T>(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            return await DeserializeToObject<T>(await Get(relativeUrl, queryParameters));
        }

        /// <summary>
        /// HTTP Get with the response deserialzed to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialze the HttpResponseMessage to</typeparam>
        /// <param name="relativeUrl">URL to make the request to</param>
        /// <param name="parameters">Query parameters for the request</param>
        /// <returns><typeparamref name="T"/> instance</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<T> Get<T>(string relativeUrl, IQueryParameters parameters)
        {
            IDictionary<string, string> dict = null;
            if (parameters != null)
            {
                var errors = parameters.GetValidationErrors();
                if (errors?.Any() ?? false)
                {
                    throw new ArgumentException(string.Join(Environment.NewLine, errors));
                }
                dict = parameters.ToQueryParameters();
            }
            return await Get<T>(relativeUrl, dict);
        }
        #endregion

        #region Put
        /// <summary>
        /// HTTP Put
        /// </summary>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameter (sent as request content, form URL encoded)</param>
        /// <returns>HttpResonseMessage</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<HttpResponseMessage> Put(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            UriBuilder builder = new(new Uri(BaseUri, relativeUrl));
            HttpRequestMessage request = new(HttpMethod.Put, builder.ToString());
            var token = await RedditAuthentication.GetBearerToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            if (RedditUserAgent != null)
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(RedditUserAgent.Name, RedditUserAgent.Version));
            }
            request.Content = new FormUrlEncodedContent(queryParameters);

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RedditApiException(response);
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return response;
        }

        /// <summary>
        /// HTTP Put with the content deserialized to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameter (sent as request content, form URL encoded)</param>
        /// <returns><typeparamref name="T"/> object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<T> Put<T>(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            return await DeserializeToObject<T>(await Put(relativeUrl, queryParameters));
        }
        #endregion

        #region Delete
        /// <summary>
        /// HTTP Delete
        /// </summary>
        /// <param name="relativeUrl">Relative URL</param>
        /// <returns>Awaitable Task</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task Delete(string relativeUrl)
        {
            UriBuilder builder = new(new Uri(BaseUri, relativeUrl));
            HttpRequestMessage request = new(HttpMethod.Delete, builder.ToString());
            var token = await RedditAuthentication.GetBearerToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            if (RedditUserAgent != null)
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(RedditUserAgent.Name, RedditUserAgent.Version));
            }

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RedditApiException(response);
            }
        }
        #endregion

        #region Post
        /// <summary>
        /// HTTP Post
        /// </summary>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameter (sent as request content, form URL encoded)</param>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<HttpResponseMessage> Post(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            UriBuilder builder = new(new Uri(BaseUri, relativeUrl));
            HttpRequestMessage request = new(HttpMethod.Post, builder.ToString());
            var token = await RedditAuthentication.GetBearerToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            if (RedditUserAgent != null)
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(RedditUserAgent.Name, RedditUserAgent.Version));
            }
            request.Content = new FormUrlEncodedContent(queryParameters);

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RedditApiException(response);
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return response;
        }

        /// <summary>
        /// HTTP Post with the content deserialized to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameter (sent as request content, form URL encoded)</param>
        /// <returns><typeparamref name="T"/> object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<T> Post<T>(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            return await DeserializeToObject<T>(await Post(relativeUrl, queryParameters));
        }
        #endregion

        #region Patch
        /// <summary>
        /// HTTP Patch
        /// </summary>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameters (sent as request content, form URL encoded)</param>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<HttpResponseMessage> Patch(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            UriBuilder builder = new(new Uri(BaseUri, relativeUrl));
            HttpRequestMessage request = new(HttpMethod.Patch, builder.ToString());
            var token = await RedditAuthentication.GetBearerToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            if (RedditUserAgent != null)
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(RedditUserAgent.Name, RedditUserAgent.Version));
            }
            request.Content = new FormUrlEncodedContent(queryParameters);

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RedditApiException(response);
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return response;
        }

        /// <summary>
        /// HTTP Patch with the content deserialized to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="relativeUrl">Relative URL</param>
        /// <param name="queryParameters">Query parameters (sent as request content, form URL encoded)</param>
        /// <returns><typeparamref name="T"/> object</returns>
        /// <exception cref="RedditApiException"></exception>
		/// <exception cref="RedditAuthenticationException"></exception>
        internal async Task<T> Patch<T>(string relativeUrl, IDictionary<string, string> queryParameters)
        {
            return await DeserializeToObject<T>(await Patch(relativeUrl, queryParameters));
        }
        #endregion
    }
}
