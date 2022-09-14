using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Users
{
    /// <summary>
    /// Users By Ids parameters
    /// </summary>
    public class UsersByIdsParameters : QueryParametersBase
    {
        /// <summary>
        /// Account fullnames
        /// </summary>
        public IEnumerable<string> Ids { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            Dictionary<string, string> parameters = new()
            {
                { "ids", string.Join(',', Ids) }
            };
            return parameters;
        }
    }

}
