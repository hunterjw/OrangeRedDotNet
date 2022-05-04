using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.Extensions
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
