using RedditDotNet.Interfaces;

namespace RedditDotNet.Models.Comments
{
    public class CommentBase : Thing<CommentBaseData>, ILinkOrComment { }
}
