using System.Collections.Generic;

namespace RedditDotNet.Interfaces
{
    /// <summary>
    /// Interface for objects used to generate HTTP query parameters
    /// </summary>
    public interface IQueryParameters
    {
        /// <summary>
        /// Create a dictionary of query parameters
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> ToQueryParameters();

        /// <summary>
        /// Validate the query parameters
        /// </summary>
        /// <exception cref="ArgumentException">When the parameters are invalid</exception>
        void Validate();
    }
}
