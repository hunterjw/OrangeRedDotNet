using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Account;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Account
{
    /// <summary>
    /// Set Preferences Parameters
    /// </summary>
    public class SetPreferencesParameters : QueryParametersBase
    {
        /// <summary>
        /// User preferences
        /// </summary>
        public Preferences Preferences { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>
            {
                { "json", Preferences.ToJson() }
            };
        }
    }
}
