using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Subreddits
{
    public enum MySubredditsType
    {
        [Description("subscriber")] Subscriber,
        [Description("contributor")] Contributor,
        [Description("moderator")] Moderator,
        [Description("streams")] Streams
    }
}
