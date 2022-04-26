using CommandLine;
using RedditDotNet.ConsoleApp.Verbs.Listings;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Parameters;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Get my subreddits
    /// </summary>
    [Verb("subreddits-get-mine", HelpText = "Get my subreddits")]
    internal class GetMineVerb : ListingVerb
    {
        /// <summary>
        /// Type of subreddits to get (subscriber, contributor, moderator, streams)
        /// </summary>
        [Option(Required = true, HelpText = "Type of subreddits to get (subscriber, contributor, moderator, streams)")]
        public string Type { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.GetMine(
                    Type.ToEnumFromDescriptionString<MySubredditsType>(), 
                    BuildListingParameters()))
                .ToJson();
        }
    }
}
