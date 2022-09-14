using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Multis
{
    /// <summary>
    /// Get Muli Parameters
    /// </summary>
    public class MultiParameters : QueryParametersBase
    {
        /// <summary>
        /// True to expand subreddits
        /// </summary>
        public bool? ExpandSubreddits { get; set; } = null;

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            Dictionary<string, string> parameters = new();
            if (ExpandSubreddits != null)
            {
                parameters.Add("expand_srs", ExpandSubreddits.ToString());
            }
            return parameters;
        }
    }
}
