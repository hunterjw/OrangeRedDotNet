using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Verb to get the best of Reddit
    /// </summary>
    [Verb("listings-get-best", HelpText = "Get the best of Reddit")]
    class GetBestVerb : ListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Listings.GetBest(parameters: BuildListingParameters()).Result.ToJson();
        }
    }
}
