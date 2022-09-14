using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Users
{
    /// <summary>
    /// Is Username Available Parameters
    /// </summary>
    public class IsUsernameAvailableParameters : QueryParametersBase
    {
        /// <summary>
        /// A valid, unused, username
        /// </summary>
        public string Username { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            Dictionary<string, string> parameters = new()
            {
                { "user", Username }
            };
            return parameters;
        }
    }
}
