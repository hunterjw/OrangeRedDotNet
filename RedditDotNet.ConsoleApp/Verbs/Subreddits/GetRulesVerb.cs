using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Get the rules for the current subreddit
    /// </summary>
    [Verb("subreddits-get-rules", HelpText = "Get the rules for the current subreddit")]
    internal class GetRulesVerb : VerbBase
    {
        /// <summary>
        /// Name of the subreddit
        /// </summary>
        [Option(Required = true, HelpText = "Name of the subreddit")]
        public string SubredditName { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.GetRules(SubredditName)).ToJson();
        }
    }
}
