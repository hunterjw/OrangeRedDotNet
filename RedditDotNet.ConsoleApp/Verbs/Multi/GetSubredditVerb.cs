using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get Subreddit info for a MultiReddit
    /// </summary>
    [Verb("multi-get-subreddit", HelpText = "Get Subreddit info for a MultiReddit")]
    internal class GetSubredditVerb : VerbBase
    {
        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <summary>
        /// Subreddit name
        /// </summary>
        [Option(Required = true, HelpText = "Subreddit name")]
        public string Subreddit { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.GetSubreddit(Path, Subreddit).Result.ToJson();
        }
    }
}
