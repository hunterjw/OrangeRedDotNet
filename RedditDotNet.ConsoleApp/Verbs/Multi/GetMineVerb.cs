using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get my MultiReddits
    /// </summary>
    [Verb("multi-get-mine", HelpText = "Get my MultiReddits")]
    internal class GetMineVerb : VerbBase
    {
        /// <summary>
        /// Expand subreddits in the results
        /// </summary>
        [Option(Required = false, HelpText = "Expand subreddits in the results")]
        public bool? ExpandSubreddits { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.GetMine(ExpandSubreddits)).ToJson();
        }
    }
}
