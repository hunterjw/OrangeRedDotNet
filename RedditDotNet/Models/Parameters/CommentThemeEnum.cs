using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
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
