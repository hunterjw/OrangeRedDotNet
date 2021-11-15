using System.ComponentModel;

namespace RedditDotNet.Models.Listings
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
