using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Multis;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Multis
{
    /// <summary>
    /// Update Multi parameters
    /// </summary>
    public class UpdateMultiParameters : MultiParameters
    {
        /// <summary>
        /// New MultiReddit model
        /// </summary>
        public MultiRedditUpdate Model { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            IDictionary<string, string> parameters = base.ToQueryParameters();
            parameters.Add("model", Model.ToJson());
            return parameters;
        }
    }
}
