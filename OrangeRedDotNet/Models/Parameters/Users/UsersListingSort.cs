using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Users
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
