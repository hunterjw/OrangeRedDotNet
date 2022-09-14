using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Multis
{
    /// <summary>
    /// Update Description parameters
    /// </summary>
    public class UpdateDescriptionParameters : QueryParametersBase
    {
        /// <summary>
        /// Description (markdown)
        /// </summary>
        public string DescriptionMd { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>
            {
                { "model", $"{{ \"body_md\": \"{DescriptionMd}\" }}" }
            };
        }
    }
}
