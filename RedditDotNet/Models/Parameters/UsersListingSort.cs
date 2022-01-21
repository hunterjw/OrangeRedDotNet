using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Sort types for User profile listings
    /// </summary>
    public enum UsersListingSort
    {
        [Description("hot")] Hot,
        [Description("new")] New,
        [Description("top")] Top,
        [Description("controversial")] Controversial,
    }
}
