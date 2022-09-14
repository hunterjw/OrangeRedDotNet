using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// Parameters to specify a thing
    /// </summary>
    public class ThingParameters : QueryParametersBase
    {
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>()
            {
                { "id", Id }
            };
        }
    }
}
