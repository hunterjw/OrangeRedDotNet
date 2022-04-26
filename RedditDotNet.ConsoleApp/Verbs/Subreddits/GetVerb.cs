using CommandLine;
using RedditDotNet.ConsoleApp.Verbs.Listings;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Parameters;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Get subreddits
    /// </summary>
    [Verb("subreddits-get", HelpText = "Get subreddits")]
    internal class GetVerb : ListingVerb
    {
        /// <summary>
        /// Type of subreddits to get (popular, new, gold, default)
        /// </summary>
        [Option(Required = true, HelpText = "Type of subreddits to get (popular, new, gold, default)")]
        public string Type { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.Get(
                    Type.ToEnumFromDescriptionString<SubredditsType>(), 
                    BuildListingParameters()))
                .ToJson();
        }
    }
}
