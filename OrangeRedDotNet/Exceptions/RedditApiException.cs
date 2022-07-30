using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models;
using System;
using System.Net;
using System.Net.Http;

namespace OrangeRedDotNet.Exceptions
{
    /// <summary>
    /// Reddit API Exception
    /// </summary>
    public class RedditApiException : Exception
    {
        #region Request
        /// <summary>
        /// Request URI
        /// </summary>
        public Uri RequestUri { get; private set; }
        /// <summary>
        /// Request Method
        /// </summary>
        public string RequestMethod { get; private set; }
        /// <summary>
        /// Request Content
        /// </summary>
        public string RequestContent { get; private set; }
        #endregion

        #region Response
        /// <summary>
        /// Response Status Code
        /// </summary>
        public HttpStatusCode ResponseStatusCode { get; private set; }
        /// <summary>
        /// Response Content string
        /// </summary>
        public string ResponseContentString { get; private set; }
        /// <summary>
        /// Response Content object
        /// </summary>
        public RedditApiError ResponseContent { get; private set; }
        #endregion

        /// <inheritdoc/>
        public override string Message
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_message))
                {
                    _message = $"{RequestMethod} request to {RequestUri} " +
                        $"failed with error code {ResponseStatusCode}{Environment.NewLine}";
                    if (!string.IsNullOrWhiteSpace(RequestContent))
                    {
                        _message += $"Request content: {RequestContent}{Environment.NewLine}";
                    }
                    if (!string.IsNullOrWhiteSpace(ResponseContentString))
                    {
                        _message += $"Response content: {ResponseContentString}{Environment.NewLine}";
                    }
                }
                return _message;
            }
        }
        private string _message;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpResponse">HTTP Response Message</param>
        public RedditApiException(HttpResponseMessage httpResponse)
        {
            RequestUri = httpResponse.RequestMessage.RequestUri;
            RequestMethod = httpResponse.RequestMessage.Method.Method;
            ResponseStatusCode = httpResponse.StatusCode;
            RequestContent = httpResponse.RequestMessage.Content?.ReadAsStringAsync().Result;
            ResponseContentString = httpResponse.Content.ReadAsStringAsync().Result;

            ResponseContent = ResponseContentString.FromJson<RedditApiError>();
        }
    }
}
