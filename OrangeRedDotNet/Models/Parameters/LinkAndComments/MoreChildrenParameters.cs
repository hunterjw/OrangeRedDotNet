using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.Listings;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// MoreChildren parameters
    /// </summary>
    public class MoreChildrenParameters : QueryParametersBase
    {
        /// <summary>
        /// Fullname of the link whose comments are being fetched
        /// </summary>
        public string LinkFullName { get; set; }
        /// <summary>
        /// List of comment ID36s that need to be fetched
        /// </summary>
        public IEnumerable<string> Children { get; set; }
        /// <summary>
        /// The sort on the comments returned
        /// </summary>
        public CommentSort? Sort { get; set; } = null;
        /// <summary>
        /// Maximum depth of subtrees in the thread to get
        /// </summary>
        public int? Depth { get; set; } = null;
        /// <summary>
        /// Only return the children requested
        /// </summary>
        public bool? LimitChildren { get; set; } = false;

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            Dictionary<string, string> parameters = new()
            {
                { "api_type", "json" },
                { "children", string.Join(',', Children) },
                { "link_id", LinkFullName },
            };
            if (Sort.HasValue)
            {
                parameters.Add("sort", Sort.Value.ToDescriptionString());
            }
            if (Depth.HasValue)
            {
                parameters.Add("depth", $"{Depth}");
            }
            if (LimitChildren ?? false)
            {
                parameters.Add("limit_children", $"{LimitChildren}");
            }
            return parameters;
        }
    }
}
