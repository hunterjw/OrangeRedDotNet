using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Fetch moderator-designated requirements to post to the subreddit.
    /// </summary>
    [Verb("subreddits-getpostrequirements", HelpText = "")]
    internal class GetPostRequirementsVerb : VerbBase
    {
        /// <summary>
        /// Subreddit display name
        /// </summary>
        [Option(Required = true, HelpText = "Subreddit display name")]
        public string SubredditName { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.GetPostRequirements(SubredditName)).ToJson();
        }
    }
}
