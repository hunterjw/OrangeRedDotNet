using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// Submit kind
    /// </summary>
    public enum SubmitKind
    {
        /// <summary>
        /// Link post
        /// </summary>
        [Description("link")] Link = 0,
        /// <summary>
        /// Self post
        /// </summary>
        [Description("self")] Self = 1,
    }
}
