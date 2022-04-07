using RedditDotNet.Interfaces;
using System;

namespace RedditDotNet.Models.Links
{
    public class Link : Thing<LinkData>, ILinkOrComment
    {
        /// <summary>
        /// Get the type of this link
        /// </summary>
        /// <returns>Link type</returns>
        public LinkType GetLinkType()
        {
            if (Data == null)
            {
                return LinkType.Unknown;
            }
            else if (!string.IsNullOrWhiteSpace(Data.PostHint) &&
                Data.PostHint.Equals("image", StringComparison.OrdinalIgnoreCase))
            {
                return LinkType.Image;
            }
            else if (Data.IsVideo)
            {
                return LinkType.Video;
            }
            else if (Data.IsGallery)
            {
                return LinkType.Gallery;
            }
            // TODO determine how to detect a poll
            //else if (???)
            //{
            //    return LinkType.Poll;
            //}
            else if (!string.IsNullOrWhiteSpace(Data.CrosspostParent))
            {
                return LinkType.Crosspost;
            }
            else if (Data.IsSelf)
            {
                return LinkType.Text;
            }
            else if ((Data.MediaEmbed != null && !string.IsNullOrWhiteSpace(Data.MediaEmbed.Content)) ||
                (Data.SecureMediaEmbed != null && !string.IsNullOrWhiteSpace(Data.SecureMediaEmbed.Content)))
            {
                return LinkType.EmbeddedMedia;
            }
            return LinkType.Link;
        }
    }
}
