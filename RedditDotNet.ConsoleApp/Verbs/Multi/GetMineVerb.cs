using CommandLine;
using RedditDotNet.Extensions;

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
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.GetMine(ExpandSubreddits).Result.ToJson();
        }
    }
}
