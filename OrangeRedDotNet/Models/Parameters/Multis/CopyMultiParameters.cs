using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.Multis
{
    /// <summary>
    /// Copy Multi parameters
    /// </summary>
    public class CopyMultiParameters : MultiParameters
    {
        /// <summary>
        /// MultiReddit path to copy from
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// MultiReddit path to copy to
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// Description (markdown)
        /// </summary>
        public string DescriptionMd { get; set; } = "";

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            var parameters = base.ToQueryParameters();
            parameters.Add("from", From);
            parameters.Add("to", To);
            if (!string.IsNullOrWhiteSpace(DisplayName))
            {
                parameters.Add("display_name", DisplayName);
            }
            if (!string.IsNullOrWhiteSpace(DescriptionMd))
            {
                parameters.Add("description_md", DescriptionMd);
            }
            return parameters;
        }
    }
}
