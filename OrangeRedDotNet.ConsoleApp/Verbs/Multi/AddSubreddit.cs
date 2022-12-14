using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to add a Subreddit to a MultiReddit
    /// </summary>
    [Verb("multi-add-subreddit", HelpText = "Add a Subreddit to a MultiReddit")]
    internal class PutSubredditVerb : VerbBase
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
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.AddSubreddit(Path, new()
            {
                SubredditName = Subreddit
            })).ToJson();
        }
    }
}
