using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Multis
{
    /// <summary>
    /// Add Subreddit parameters
    /// </summary>
    public class AddSubredditParameters : QueryParametersBase
    {
        /// <summary>
        /// Subreddit name
        /// </summary>
        public string SubredditName { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>
            {
                { "model", $"{{ \"name\": \"{SubredditName}\" }}" }
            };
        }
    }
}
