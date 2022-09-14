using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Search
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
