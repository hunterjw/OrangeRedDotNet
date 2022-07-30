using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Get duplicates for a given Link
    /// </summary>
    [Verb("listings-get-duplicates", HelpText = "Get duplicates for a given article")]
    class GetDuplicatesVerb : ListingVerb
    {
        /// <summary>
        /// Link ID
        /// </summary>
        [Option(Required = true, HelpText = "Link ID")]
        public string ArticleId { get; set; }
        /// <summary>
        /// To get crossposts only or not
        /// </summary>
        [Option(HelpText = "To get crossposts only or not")]
        public bool? CrosspostsOnly { get; set; }
        /// <summary>
        /// The sort for the returned results
        /// </summary>
        [Option(HelpText = "The sort for the returned results")]
        public string Sort { get; set; }
        /// <summary>
        /// Subreddit
        /// </summary>
        [Option(HelpText = "Subreddit name")]
        public string Subreddit { get; set; }

        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Listings.GetDuplicates(ArticleId, BuildDuplicateListingParameters())).ToJson();
        }

        /// <summary>
        /// Build a duplicate listing parameters object
        /// </summary>
        /// <returns>Duplicate listing parameters object</returns>
        private DuplicateListingParameters BuildDuplicateListingParameters()
        {
            return new DuplicateListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                CrosspostsOnly = CrosspostsOnly,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                ShowAll = ShowAll,
                Sort = Sort.ToEnumFromDescriptionString<DuplicateSort>(),
                Subreddit = Subreddit
            };
        }
    }
}
