using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum SearchSort
    {
        [Description("relavance")] Relavance,
        [Description("hot")] Hot,
        [Description("top")] Top,
        [Description("new")] New,
        [Description("comments")] Comments
    }
}
