using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum SubredditsType
    {
        [Description("popular")] Popular,
        [Description("new")] New,
        [Description("gold")] Gold,
        [Description("default")] Default
    }
}
