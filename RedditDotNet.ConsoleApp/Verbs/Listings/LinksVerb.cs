using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Parameters;
using System;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Get a listing of links
    /// </summary>
    [Verb("listings-get-links", HelpText = "Get a listing of links")]
    internal class LinksVerb : ListingVerb
    {
        /// <summary>
        /// Subreddit to get links for
        /// </summary>
        [Option(HelpText = "Subreddit to get links for")]
        public string Subreddit { get; set; }
        /// <summary>
        /// Type of listing to get (best, hot, new, rising, controversial, top, random)
        /// </summary>
        [Option(HelpText = "Type of listing to get (best, hot, new, rising, controversial, top, random)")]
        public string ListingType { get; set; }
        /// <summary>
        /// Path of MultiReddit to get links for
        /// </summary>
        [Option(HelpText = "Path of MultiReddit to get links for")]
        public string MultiRedditPath { get; set; }
        
        public override async Task<string> Run(Reddit reddit)
        {
            ListingType = ListingType?.ToLower();
            if (string.IsNullOrWhiteSpace(ListingType))
            {
                if (string.IsNullOrWhiteSpace(Subreddit) && string.IsNullOrWhiteSpace(MultiRedditPath))
                {
                    ListingType = "best";
                }
                else
                {
                    ListingType = "hot";
                }
            }

            Listing<Link> links = ListingType switch
            {
                "best" => await reddit.Listings.GetBest(BuildListingParameters()),
                "hot" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    await reddit.Listings.GetHot(BuildLocationListingParameters(), Subreddit) :
                    await reddit.Listings.GetHot(MultiRedditPath, BuildLocationListingParameters()),
                "new" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    await reddit.Listings.GetNew(BuildListingParameters(), Subreddit) :
                    await reddit.Listings.GetNew(MultiRedditPath, BuildListingParameters()),
                "rising" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    await reddit.Listings.GetRising(BuildListingParameters(), Subreddit) :
                    await reddit.Listings.GetRising(MultiRedditPath, BuildListingParameters()),
                "controversial" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    await reddit.Listings.GetControversial(BuildSortListingParameters(), Subreddit) :
                    await reddit.Listings.GetControversial(MultiRedditPath, BuildSortListingParameters()),
                "top" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    await reddit.Listings.GetTop(BuildSortListingParameters(), Subreddit) :
                    await reddit.Listings.GetTop(MultiRedditPath, BuildSortListingParameters()),
                "random" => reddit.Listings.GetRandom(Subreddit).Result,
                _ => throw new ArgumentException($"Invalid {nameof(ListingType)}"),
            };
            return links.ToJson();
        }

        /// <summary>
        /// Build a LocationListingParameters object based on the current component parameters
        /// </summary>
        /// <returns>LocationListingParameters object</returns>
        private LocationListingParameters BuildLocationListingParameters()
        {
            return new LocationListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                Limit = Limit,
                ShowAll = ShowAll,
                ExpandSubreddits = ExpandSubreddits
            };
        }

        /// <summary>
        /// Build a SortListingParameters object based on the current component parameters
        /// </summary>
        /// <returns>SortListingParameters object</returns>
        private SortListingParameters BuildSortListingParameters()
        {
            return new SortListingParameters
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
