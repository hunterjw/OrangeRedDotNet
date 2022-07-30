using CommandLine;
using OrangeRedDotNet.ConsoleApp.Verbs.Listings;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Search
{
    /// <summary>
    /// Search Reddit
    /// </summary>
    [Verb("search", HelpText = "Search Reddit")]
    internal class SearchVerb : ListingVerb
    {
        /// <summary>
        /// Subreddit name
        /// </summary>
        [Option("subreddit", HelpText = "Subreddit name")]
        public string Subreddit { get; set; }
        /// <summary>
        /// Search query string
        /// </summary>
        [Option("query", HelpText = "Search query string")]
        public string Query { get; set; }
        /// <summary>
        /// To restrict the results to a single subreddit or not
        /// </summary>
        [Option("restrictsubreddit", HelpText = "To restrict the results to a single subreddit or not")]
        public bool? RestrictSubreddit { get; set; }
        /// <summary>
        /// Sort of the results, can be relavance, hot, top, new, comments
        /// </summary>
        [Option("sort", HelpText = "Sort of the results, can be relavance, hot, top, new, or comments")]
        public string Sort { get; set; }
        /// <summary>
        /// Timescale of the results, can be hour, day, week, month, year, or all
        /// </summary>
        [Option("timescale", HelpText = "Timescale of the results, can be hour, day, week, month, year, or all")]
        public string Timescale { get; set; }
        /// <summary>
        /// Type of the results, can be one or more of sr, link, user
        /// </summary>
        [Option("type", HelpText = "Type of the results, can be one or more of sr, link, user", Separator = ',')]
        public IEnumerable<string> Type { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Search.Search(BuildParameters(), Subreddit)).ToJson();
        }

        /// <summary>
        /// Build parameters object
        /// </summary>
        /// <returns>Parameters object</returns>
        private SearchListingParameters BuildParameters()
        {
            SearchType? type = null;
            foreach (var value in Type)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (type == null)
                    {
                        type = value.ToEnumFromDescriptionString<SearchType>();
                    }
                    else
                    {
                        type |= value.ToEnumFromDescriptionString<SearchType>();
                    }
                }
            }
            return new SearchListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                Query = Query,
                RestrictSubreddit = RestrictSubreddit,
                ShowAll = ShowAll,
                Sort = string.IsNullOrWhiteSpace(Sort) ? null 
                    : Sort.ToEnumFromDescriptionString<SearchSort>(),
                Timescale = string.IsNullOrWhiteSpace(Timescale) ? null 
                    : Timescale.ToEnumFromDescriptionString<Timescale>(),
                Type = type
            };
        }
    }
}
