using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Models.Listings;
using System.Collections.Generic;
using System.Linq;

namespace OrangeRedDotNet.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<CommentBase> NestComments(this IEnumerable<CommentBase> comments, string parentId)
        {
            foreach (var child in comments)
            {
                if (child.Data is CommentData commentData)
                {
                    var grandChildren = comments.Where(_ => _.Data.ParentId.Equals(commentData.Name)).ToList();
                    if (grandChildren.Any())
                    {
                        commentData.Replies = new Listing<CommentBase>
                        {
                            Data = new ListingData<CommentBase>
                            {
                                Children = grandChildren
                            },
                            Kind = "Listing"
                        };
                    }
                }
            }
            return comments.Where(_ => _.Data.ParentId.Equals(parentId));
        }
    }
}
