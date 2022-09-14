using OrangeRedDotNet.Interfaces;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters
{
    /// <summary>
    /// Base Query Parameters class
    /// </summary>
    public abstract class QueryParametersBase : IQueryParameters
    {
        /// <inheritdoc/>
        public virtual IList<string> GetValidationErrors()
        {
            return new List<string>();
        }

        /// <inheritdoc/>
        public abstract IDictionary<string, string> ToQueryParameters();
    }
}
