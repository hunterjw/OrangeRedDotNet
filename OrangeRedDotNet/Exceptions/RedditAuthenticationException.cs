using System;

namespace OrangeRedDotNet.Exceptions
{
    /// <summary>
    /// Reddit authentication exception
    /// </summary>
    public class RedditAuthenticationException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public RedditAuthenticationException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
