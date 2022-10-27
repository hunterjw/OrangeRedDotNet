using System.Collections.Generic;
using OrangeRedDotNet.Models.Parameters;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// Comment parameters
    /// </summary>
    public class CommentParameters : QueryParametersBase
    {
        /// <summary>
        /// Fullname of parent thing
        /// </summary>
        public string ThingId { get; set; }
        /// <summary>
        /// Raw markdown text
        /// </summary>
        public string Text { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>
            {
                { "api_type", "json" },
                { "thing_id", ThingId },
                { "text", Text }
            };
        }
    }
}
