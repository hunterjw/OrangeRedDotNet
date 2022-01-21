using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Returned object type filter for Userp profile listings
    /// </summary>
    public enum UsersListingType
    {
        [Description("links")] Links,
        [Description("comments")] Comments,
    }
}
