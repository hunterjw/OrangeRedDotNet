using CommandLine;

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
            return ToJson(reddit.Listings.GetBest(parameters: BuildListingParameters()).Result);
        }
    }
}
