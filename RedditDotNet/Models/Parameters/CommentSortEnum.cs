using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Comment sort enum
    /// </summary>
    public enum CommentSort
    {
        [Description("confidence")] Confidence,
        [Description("top")] Top,
        [Description("new")] New,
        [Description("controversial")] Controversial,
        [Description("old")] Old,
        [Description("random")] Random,
        [Description("qa")] QA,
        [Description("live")] Live
    }
}
