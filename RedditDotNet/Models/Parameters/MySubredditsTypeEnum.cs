using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum MySubredditsType
    {
        [Description("subscriber")] Subscriber,
        [Description("contributor")] Contributor,
        [Description("moderator")] Moderator,
        [Description("streams")] Streams
    }
}
