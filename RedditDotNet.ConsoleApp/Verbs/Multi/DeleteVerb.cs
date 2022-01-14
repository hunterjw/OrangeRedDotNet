using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to delete a MultiReddit
    /// </summary>
    [Verb("multi-delete", HelpText = "Delete a MultiReddit")]
    internal class DeleteVerb : VerbBase
    {
        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            reddit.Multis.DeleteMulti(Path).Wait();
            return string.Empty;
        }
    }
}
