using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get MultiReddits by username
    /// </summary>
    [Verb("multi-get-by-username", HelpText = "Get MultiReddits by username")]
    internal class GetByUsernameVerb : VerbBase
    {
        /// <summary>
        /// Expand subreddit details
        /// </summary>
        [Option(Required = false, HelpText = "Expand subreddit details")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// Reddit username
        /// </summary>
        [Option(Required = true, HelpText = "Reddit username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.GetByUsername(Username, ExpandSubreddits).Result.ToJson();
        }
    }
}
