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
        /// Get a preview image URL
        /// </summary>
        protected string PreviewUrl
        {
            get
            {
                if (Link?.Data?.Preview?.Images?.Count > 0)
                {
                    PreviewImage image = Link.Data.Preview.Images.First();
                    if ((Link?.Data?.Over18 ?? false) && (image.Variants?.ContainsKey("nsfw") ?? false))
                    {
                        image = image.Variants["nsfw"];
                    }
                    if ((Link?.Data?.Spoiler ?? false) && (image.Variants?.ContainsKey("obfuscated") ?? false))
                    {
                        image = image.Variants["obfuscated"];
                    }
                    return HttpUtility.HtmlDecode(image.Resolutions.Last().Url);
                }
                // TODO replace this with locally hosted resource
                return "https://via.placeholder.com/256";
            }
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            LinkType linkType = Link.GetLinkType();
            if (!ContentCollapsed &&
                !(linkType == LinkType.Image ||
                linkType == LinkType.Gallery ||
                linkType == LinkType.Video ||
                linkType == LinkType.Text))
            {
                ContentCollapsed = true;
            }
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
    }
}
