using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Subreddits
{
    public enum SubredditsType
    {
        [Description("popular")] Popular,
        [Description("new")] New,
        [Description("gold")] Gold,
        [Description("default")] Default
    }
}
