using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get a MultiReddit
    /// </summary>
    [Verb("multi-get", HelpText = "Get a MultiReddit")]
    internal class GetVerb : VerbBase
    {
        /// <summary>
        /// Expand Subreddit details in the output
        /// </summary>
        [Option(Required = false, HelpText = "Expand Subreddit details in the output")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.GetMulti(Path, ExpandSubreddits).Result.ToJson();
        }
    }
}
