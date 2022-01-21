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
        /// Get a list of validation errors for the parameters
        /// </summary>
        /// <returns>List of errors</returns>
        IList<string> GetValidationErrors();
    }
}
