using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Subreddits
{
    /// <summary>
    /// Subscribe parameters
    /// </summary>
    public class SubscribeParameters : QueryParametersBase
    {
        /// <summary>
        /// Subreddit name
        /// </summary>
        public string SubredditName { get; set; }
        /// <summary>
        /// Subscribe action
        /// </summary>
        public SubscribeAction Action { get; set; }
        /// <summary>
        /// Set to True to prevent automatically subscribing the user to the current set of 
        /// defaults when they take their first subscription action. Attempting to set it 
        /// for an unsubscribe action will result in an error.
        /// </summary>
        public bool SkipInitialDefaults { get; set; } = false;

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>()
            {
                { "sr_name", SubredditName },
                { "action", Action.ToDescriptionString() },
                { "skip_initial_defaults", $"{SkipInitialDefaults}" },
            };
        }
    }
}
