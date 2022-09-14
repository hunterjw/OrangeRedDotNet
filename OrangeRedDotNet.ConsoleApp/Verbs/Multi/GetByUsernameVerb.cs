using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get MultiReddits by username
    /// </summary>
    [Verb("multi-get-by-username", HelpText = "Get MultiReddits by username")]
    internal class GetByUsernameVerb : VerbBase
    {
        /// <summary>
        /// Expand subreddit details
        /// </summary>
        [Option(Required = false, HelpText = "Expand subreddit details")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// Reddit username
        /// </summary>
        [Option(Required = true, HelpText = "Reddit username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.GetByUsername(Username, new()
            {
                ExpandSubreddits = ExpandSubreddits
            })).ToJson();
        }
    }
}
