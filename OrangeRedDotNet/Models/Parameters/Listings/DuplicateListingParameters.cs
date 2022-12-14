using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Listings
{
    /// <summary>
    /// Listing parameters for getting duplicate links
    /// </summary>
    public class DuplicateListingParameters : ListingParameters
    {
        /// <summary>
        /// To return crossposts only
        /// </summary>
        public bool? CrosspostsOnly { get; set; }
        /// <summary>
        /// Sort for the returned links
        /// </summary>
        public DuplicateSort? Sort { get; set; }
        /// <summary>
        /// Subreddit name
        /// </summary>
        public string Subreddit { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            IDictionary<string, string> dict = base.ToQueryParameters();
            if (CrosspostsOnly.HasValue)
            {
                dict.Add("crossposts_only", CrosspostsOnly.Value.ToString());
            }
            if (Sort.HasValue)
            {
                dict.Add("sort", Sort.Value.ToDescriptionString());
            }
            if (!string.IsNullOrWhiteSpace(Subreddit))
            {
                dict.Add("sr", Subreddit);
            }
            return dict;
        }

        /// <inheritdoc/>
        public override ListingParameters Copy()
        {
            return new DuplicateListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                ShowAll = ShowAll,
                CrosspostsOnly = CrosspostsOnly,
                Sort = Sort,
                Subreddit = Subreddit,
            };
        }
    }
}
