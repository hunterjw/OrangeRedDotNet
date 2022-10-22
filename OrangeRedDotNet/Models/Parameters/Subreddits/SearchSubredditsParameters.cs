using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Subreddits
{
    /// <summary>
    /// Search subreddits parameters
    /// </summary>
    public class SearchSubredditsParameters : QueryParametersBase
    {
        /// <summary>
        /// Query string
        /// </summary>
        public string Query { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>
            {
                { "query", Query }
            };
        }
    }
}
