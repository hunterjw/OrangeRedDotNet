using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Users
{
    /// <summary>
    /// Update Friend parameters
    /// </summary>
    public class UpdateFriendParameters : QueryParametersBase
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// A string no longer than 300 characters
        /// </summary>
        public string Note { get; set; } = "";

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>()
            {
                {
                    "json",
                    $"{{ \"name\": \"{Username}\"" +
                        (!string.IsNullOrWhiteSpace(Note) ? $", \"note\": \"{Note}\"" : "") +
                        $" }}"
                }
            };
        }
    }
}
