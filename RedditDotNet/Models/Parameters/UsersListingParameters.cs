using RedditDotNet.Extensions;
using System.Collections.Generic;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Parameters for user profile listings
    /// </summary>
    public class UsersListingParameters : ListingParameters
    {
        /// <summary>
        /// Context to include for comments
        /// </summary>
        public int? Context { get; set; }
        /// <summary>
        /// Sort of the results
        /// </summary>
        public UsersListingSort? Sort { get; set; }
        /// <summary>
        /// Timescale of results (when sort is top or controversial)
        /// </summary>
        public Timescale? Timescale { get; set; }
        /// <summary>
        /// Limit for what type of results to return
        /// </summary>
        public UsersListingType? Type { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            var parameters = base.ToQueryParameters();
            if (Context.HasValue)
            {
                parameters.Add("context", Context.Value.ToString());
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
                parameters.Add("type", Type.Value.ToDescriptionString());
            }
            return parameters;
        }

        /// <inheritdoc/>
        public override IList<string> GetValidationErrors()
        {
            var errors = base.GetValidationErrors();
            if (Context.HasValue && (Context.Value < 2 || Context.Value > 10))
            {
                errors.Add($"{nameof(Context)} must be between 2 and 10");
            }
            return errors;
        }

        /// <inheritdoc/>
        public override ListingParameters Copy()
        {
            return new UsersListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                ExpandSubreddits = ExpandSubreddits,
                Limit = Limit,
                ShowAll = ShowAll,
                Context = Context,
                Sort = Sort,
                Timescale = Timescale,
                Type = Type,
            };
        }
    }
}
