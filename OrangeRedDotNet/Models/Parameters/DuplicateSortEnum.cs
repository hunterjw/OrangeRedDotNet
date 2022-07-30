using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters
{
    /// <summary>
    /// Sort for duplicate Links
    /// </summary>
    public enum DuplicateSort
    {
        [Description("num_comments")] NumberOfComments,
        [Description("new")] New
    }
}
