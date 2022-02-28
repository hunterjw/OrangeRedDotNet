using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Multis;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to update a MultiReddit
    /// </summary>
    [Verb("multi-update", HelpText = "Update a MultiReddit")]
    internal class UpdateVerb : VerbBase
    {
        /// <summary>
        /// Expand the Subreddits in the result
        /// </summary>
        [Option(HelpText = "Expand the Subreddits in the result")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <summary>
        /// Json object of the MultiReddit, see https://www.reddit.com/dev/api/#POST_api_multi_{multipath}
        /// </summary>
        [Option(Required = true, HelpText = "Json object of the MultiReddit, see https://www.reddit.com/dev/api/#POST_api_multi_{multipath}")]
        public string Model { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.UpdateMulti(Path, Model.FromJson<MultiRedditUpdate>(), ExpandSubreddits)).ToJson();
        }
    }
}
