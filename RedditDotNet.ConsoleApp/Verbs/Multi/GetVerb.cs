using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

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
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.GetMulti(Path, ExpandSubreddits)).ToJson();
        }
    }
}
