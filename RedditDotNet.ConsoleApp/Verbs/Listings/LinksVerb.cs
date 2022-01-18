using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System;

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
        
        public override string Run(Reddit reddit)
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
                "best" => reddit.Listings.GetBest(BuildListingParameters()).Result,
                "hot" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    reddit.Listings.GetHot(BuildLocationListingParameters(), Subreddit).Result :
                    reddit.Listings.GetHot(MultiRedditPath, BuildLocationListingParameters()).Result,
                "new" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    reddit.Listings.GetNew(BuildListingParameters(), Subreddit).Result :
                    reddit.Listings.GetNew(MultiRedditPath, BuildListingParameters()).Result,
                "rising" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    reddit.Listings.GetRising(BuildListingParameters(), Subreddit).Result :
                    reddit.Listings.GetRising(MultiRedditPath, BuildListingParameters()).Result,
                "controversial" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    reddit.Listings.GetControversial(BuildSortListingParameters(), Subreddit).Result :
                    reddit.Listings.GetControversial(MultiRedditPath, BuildSortListingParameters()).Result,
                "top" => string.IsNullOrWhiteSpace(MultiRedditPath) ? 
                    reddit.Listings.GetTop(BuildSortListingParameters(), Subreddit).Result :
                    reddit.Listings.GetTop(MultiRedditPath, BuildSortListingParameters()).Result,
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
