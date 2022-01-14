using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to delete a Subreddit from a MultiReddit
    /// </summary>
    [Verb("multi-delete-subreddit", HelpText = "Delete a Subreddit from a MultiReddit")]
    internal class DeleteSubredditVerb : VerbBase
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
            reddit.Multis.DeleteSubreddit(Path, Subreddit).Wait();
            return string.Empty;
        }
    }
}
