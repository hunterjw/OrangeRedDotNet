using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum SearchType
    {
        [Description("sr")] Subreddit = 1,
        [Description("link")] Link = 2,
        [Description("user")] User = 4
    }
}
