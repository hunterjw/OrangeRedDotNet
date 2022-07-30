using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters
{
    /// <summary>
    /// Search listing parameters
    /// </summary>
    public class SearchListingParameters : ListingParameters
    {
        /// <summary>
        /// Search query string
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// To restrict the results to a single subreddit or not
        /// </summary>
        public bool? RestrictSubreddit { get; set; }
        /// <summary>
        /// Sort of the results
        /// </summary>
        public SearchSort? Sort { get; set; }
        /// <summary>
        /// Timescale of the results
        /// </summary>
        public Timescale? Timescale { get; set; }
        /// <summary>
        /// Type of the results
        /// </summary>
        public SearchType? Type { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            var parameters = base.ToQueryParameters();
            if (!string.IsNullOrWhiteSpace(Query))
            {
                parameters.Add("q", Query);
            }
            if (RestrictSubreddit.HasValue)
            {
                parameters.Add("restrict_sr", RestrictSubreddit.ToString());
            }
            if (Sort.HasValue)
            {
                parameters.Add("sort", Sort.Value.ToDescriptionString());
            }
            if (Timescale.HasValue)
            {
                parameters.Add("t", Timescale.Value.ToDescriptionString());
            }
            if (Type.HasValue)
            {
                List<string> types = new();
                if (Type.Value.HasFlag(SearchType.Subreddit))
                {
                    types.Add("sr");
                }
                if (Type.Value.HasFlag(SearchType.Link))
                {
                    types.Add("link");
                }
                if (Type.Value.HasFlag(SearchType.User))
                {
                    types.Add("user");
                }
                parameters.Add("type", string.Join(',', types));
            }
            return parameters;
        }
        
        /// <inheritdoc/>
        public override ListingParameters Copy()
        {
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
                Sort = Sort,
                Timescale = Timescale,
                Type = Type,
            };
        }
    }
}
