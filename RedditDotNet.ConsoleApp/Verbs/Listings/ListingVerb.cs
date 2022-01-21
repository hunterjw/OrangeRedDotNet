using CommandLine;
using RedditDotNet.Models.Parameters;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Base class for (most) listing verbs
    /// </summary>
    abstract class ListingVerb : VerbBase
    {
        /// <summary>
        /// Link fullname
        /// </summary>
        [Option(HelpText = "Link fullname")]
        public string After { get; set; }
        /// <summary>
        /// Link fullname
        /// </summary>
        [Option(HelpText = "Link fullname")]
        public string Before { get; set; }
        /// <summary>
        /// Number of Links already seen
        /// </summary>
        [Option(Default = 0, HelpText = "Number of items already seen")]
        public int Count { get; set; }
        /// <summary>
        /// Number of Links to return
        /// </summary>
        [Option(Default = 25, HelpText = "Number of Links to return")]
        public int Limit { get; set; }
        /// <summary>
        /// Set to true to override preferences that would hide Links
        /// </summary>
        [Option(HelpText = "Set to true to override preferences that would hide Links")]
        public bool? ShowAll { get; set; }
        /// <summary>
        /// Set to true to expand linked subreddits
        /// </summary>
        [Option(HelpText = "Set to true to expand linked subreddits")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// Build a listing parameters object
        /// </summary>
        /// <returns>Listing parameters object</returns>
        protected ListingParameters BuildListingParameters()
        {
            return new ListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                Limit = Limit,
                ShowAll = ShowAll,
                ExpandSubreddits = ExpandSubreddits
            };
        }
    }
}
