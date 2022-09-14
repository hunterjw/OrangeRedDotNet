using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Search
{
    public enum SearchType
    {
        [Description("sr")] Subreddit = 1,
        [Description("link")] Link = 2,
        [Description("user")] User = 4
    }
}
