using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Models.Links;
using System.Linq;
using System.Web;

namespace RedditDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Component to display a link as a card
    /// </summary>
    public partial class LinkCard
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Link to display
        /// </summary>
        [Parameter]
        public Link Link { get; set; }

        /// <summary>
        /// If the content of the Link is collapsed or not
        /// </summary>
        [Parameter]
        public bool ContentCollapsed { get; set; } = true;

        /// <summary>
        /// If the spoiler has been acknowledged or not
        /// </summary>
        protected bool SpoilerAcknowledged { get; set; }

        /// <summary>
        /// If NSFW has been acknowledged or not
        /// </summary>
        protected bool NsfwAcknowledged { get; set; }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            LinkType linkType = Link.GetLinkType();
            if (!ContentCollapsed &&
                !(linkType == LinkType.Image ||
                linkType == LinkType.Gallery ||
                linkType == LinkType.Video ||
                linkType == LinkType.Text ||
                linkType == LinkType.Crosspost ||
                linkType == LinkType.EmbeddedMedia))
            {
                ContentCollapsed = true;
            }
        }

        /// <summary>
        /// Helper function to get a preview imager URL
        /// </summary>
        /// <param name="link">Link object</param>
        /// <returns>preview URL</returns>
        protected string GetPreviewUrl(Link link)
        {
            LinkType linkType = link.GetLinkType();
            if (link?.Data?.Preview?.Images?.Count > 0)
            {
                PreviewImage image = link.Data.Preview.Images.First();
                if ((link?.Data?.Over18 ?? false) && (image.Variants?.ContainsKey("nsfw") ?? false))
                {
                    image = image.Variants["nsfw"];
                }
                if ((link?.Data?.Spoiler ?? false) && (image.Variants?.ContainsKey("obfuscated") ?? false))
                {
                    image = image.Variants["obfuscated"];
                }
                return HttpUtility.HtmlDecode(image.Resolutions.Last().Url);
            }

            if (linkType == LinkType.Gallery)
            {
                MediaMetadata first = link.Data.MediaMetadata.Values.First();
                MediaMetadataImage image;
                if (first.Obscured?.Count > 0)
                {
                    image = first.Obscured.First();
                }
                else
                {
                    image = first.Previews.First();
                }
                return HttpUtility.HtmlDecode(image.Url);
            }
            else if (linkType == LinkType.Crosspost)
            {
                return GetPreviewUrl(new Link
                {
                    Kind = link.Kind,
                    Data = link.Data.CrosspostParentList.First()
                });
            }

            // TODO replace this with locally hosted resource
            return "https://via.placeholder.com/256";
        }

        /// <summary>
        /// Get the markup string for this links embedded media
        /// </summary>
        /// <returns>HTML markup string</returns>
        public MarkupString GetEmbeddedMediaContent()
        {
            string content = null;
            int width = 0;
            int height = 0;
            if (Link.Data.SecureMediaEmbed != null)
            {
                content = HttpUtility.HtmlDecode(Link.Data.SecureMediaEmbed.Content);
                width = Link.Data.SecureMediaEmbed.Width;
                height = Link.Data.SecureMediaEmbed.Height;
            }
            else if (Link.Data.MediaEmbed != null)
            {
                content = HttpUtility.HtmlDecode(Link.Data.MediaEmbed.Content);
                width = Link.Data.MediaEmbed.Width;
                height = Link.Data.MediaEmbed.Height;
            }
            if (!string.IsNullOrWhiteSpace(content))
            {
                return (MarkupString)($"<div " +
                    $"class=\"rounded\" " +
                    $"style=\"width:{width}px; height:{height}px; " +
                    $"position:relative; overflow:hidden;\">" +
                    $"{content}" +
                    $"</div>");
            }
            return default;
        }

        /// <summary>
        /// Get a RedditVideo object for the current link (if there is one)
        /// </summary>
        /// <returns>RedditVideo object</returns>
        protected RedditVideo GetRedditVideoObject()
        {
            if (Link.GetLinkType() == LinkType.Video)
            {
                if (Link.Data.SecureMedia != null)
                {
                    return Link.Data.SecureMedia.RedditVideo;
                }
                else if (Link.Data.Media != null)
                {
                    return Link.Data.Media.RedditVideo;
                }
            }
            return null;
        }

        /// <summary>
        /// OnClick handler for buttons that toggle the collapsable region
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected void ContentCollapsedButton_OnClick(MouseEventArgs e)
        {
            ContentCollapsed = !ContentCollapsed;
        }

        /// <summary>
        /// On click event handler for the comments button
        /// </summary>
        /// <param name="e"></param>
        protected void CommentsButton_OnClick(MouseEventArgs e)
        {
            NavigationManager.NavigateTo($"/r/{Link.Data.Subreddit}/comments/{Link.Data.Id}");
        }

        /// <summary>
        /// Double click event handler for the card
        /// </summary>
        /// <param name="e">Mouse event args</param>
        protected void Card_OnDblClick(MouseEventArgs e)
        {
            LinkType linkType = Link.GetLinkType();
            if (linkType == LinkType.Image ||
                linkType == LinkType.Gallery ||
                linkType == LinkType.Video ||
                linkType == LinkType.Text)
            {
                ContentCollapsed = !ContentCollapsed;
            }
        }

        /// <summary>
        /// Button clicked event handler for the spoiler button
        /// </summary>
        /// <param name="e">Mouse event args</param>
        protected void SpoilerButton_OnClick(MouseEventArgs e)
        {
            SpoilerAcknowledged = !SpoilerAcknowledged;
        }

        /// <summary>
        /// Button clicked event handler for the NSFW button
        /// </summary>
        /// <param name="e">Mouse event args</param>
        protected void NsfwButton_OnClick(MouseEventArgs e)
        {
            NsfwAcknowledged = !NsfwAcknowledged;
        }

        /// <summary>
        /// Button clicked event handler for the duplicates btton
        /// </summary>
        /// <param name="e">Mouse event args</param>
        protected void DuplicatesButton_OnClick(MouseEventArgs e)
        {
            NavigationManager.NavigateTo($"/r/{Link.Data.Subreddit}/duplicates/{Link.Data.Id}");
        }
    }
}
