using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Listings;
using System;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Get a listing of links
    /// </summary>
    [Verb("listings-get-links", HelpText = "Get a listing of links")]
    internal class GetLinksVerb : ListingVerb
    {
        /// <summary>
        /// Subreddit to get links for
        /// </summary>
        [Option(HelpText = "Subreddit to get links for")]
        public string Subreddit { get; set; }
        /// <summary>
        /// Type of listing to get (best, hot, new, rising, controversial, top)
        /// </summary>
        [Option(HelpText = "Type of listing to get (best, hot, new, rising, controversial, top)")]
        public string ListingType { get; set; }
        /// <summary>
        /// Path of MultiReddit to get links for
        /// </summary>
        [Option(HelpText = "Path of MultiReddit to get links for")]
        public string MultiRedditPath { get; set; }
        
        /// <inheritdoc/>
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

            Listing<Link> links;
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                links = await reddit.Listings.GetLinksForSubreddit(
                    ListingType.ToEnumFromDescriptionString<LinkListingType>(),
                    Subreddit,
                    BuildParameters(ListingType));
            }
            else if (!string.IsNullOrWhiteSpace(MultiRedditPath))
            {
                links = await reddit.Listings.GetLinksForMultireddit(
                    ListingType.ToEnumFromDescriptionString<LinkListingType>(),
                    Subreddit,
                    BuildParameters(ListingType));
            }
            else
            {
                links = await reddit.Listings.GetLinks(
                    ListingType.ToEnumFromDescriptionString<FrontPageListingType>(),
                    BuildParameters(ListingType));
            }
            return links.ToJson();
        }

        /// <summary>
        /// Build a ListingParameters object based on current component parameters
        /// </summary>
        /// <param name="listingType">Listing type</param>
        /// <returns>ListingParameters object</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="listingType"/> is invalid</exception>
        protected ListingParameters BuildParameters(string listingType)
        {
            return listingType switch
            {
                "best" => BuildListingParameters(),
                "hot" => BuildLocationListingParameters(),
                "new" => BuildListingParameters(),
                "rising" => BuildListingParameters(),
                "controversial" => BuildSortListingParameters(),
                "top" => BuildSortListingParameters(),
                _ => throw new ArgumentException($"Invalid {nameof(ListingType)}")
            };
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
