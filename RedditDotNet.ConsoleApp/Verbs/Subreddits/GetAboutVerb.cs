using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Return information about the subreddit.
    /// </summary>
    [Verb("subreddits-get-about", HelpText = "Return information about the subreddit.")]
    internal class GetAboutVerb : VerbBase
    {
        /// <summary>
        /// Name of the subreddit
        /// </summary>
        [Option(Required = true, HelpText = "Name of the subreddit")]
        public string SubredditName { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.GetAbout(SubredditName)).ToJson();
        }
    }
}
