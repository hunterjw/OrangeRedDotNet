using System.ComponentModel;

namespace RedditDotNet.Models.Listings
{
    /// <summary>
    /// Comment theme enum
    /// </summary>
    public enum CommentTheme
    {
        [Description("default")] Default,
        [Description("dark")] Dark
    }
}
