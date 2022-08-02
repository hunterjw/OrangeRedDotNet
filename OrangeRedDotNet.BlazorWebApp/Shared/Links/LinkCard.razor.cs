using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Account;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Subreddits;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Component to display a link as a card
    /// </summary>
    public partial class LinkCard
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// ToastService
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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
        /// Subreddit context
        /// Should be populated when the link is shown in a subreddit context
        /// </summary>
        [Parameter]
        public Subreddit Subreddit { get; set; }

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
                (!(linkType == LinkType.Image ||
                linkType == LinkType.Gallery ||
                linkType == LinkType.Video ||
                linkType == LinkType.Text ||
                linkType == LinkType.Crosspost ||
                linkType == LinkType.EmbeddedMedia) || 
                (linkType == LinkType.Text && string.IsNullOrWhiteSpace(Link.Data.Selftext))))
            {
                ContentCollapsed = true;
            }
            
        }

        /// <summary>
        /// Helper function to get a preview image URL
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
                if (image.Resolutions?.Count > 0)
                {
                    return HttpUtility.HtmlDecode(image.Resolutions.Last().Url);
                }
                if (image.Source != null)
                {
                    return HttpUtility.HtmlDecode(image.Source.Url);
                }
            }

            if (linkType == LinkType.Gallery && link.Data.MediaMetadata?.Count > 0)
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
            return "icon-512.png";
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
        /// Double click event handler for the card
        /// </summary>
        /// <param name="e">Mouse event args</param>
        protected void Card_OnDblClick()
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
        /// OnClick handler for the SaveToggleButton
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task SaveToggleButton_OnClick()
        {
            try
            {
                Reddit client = RedditService.GetClient();
                if (Link.Data.Saved)
                {
                    await client.LinksAndComments.Unsave(Link.Data.Name);
                }
                else
                {
                    await client.LinksAndComments.Save(Link.Data.Name);
                }
                Link.Data.Saved = !Link.Data.Saved;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Failed to update link save state"));   
            }
        }

        /// <summary>
        /// OnClick handler for the HideToggleButton
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task HideToggleButton_OnClick()
        {
            try
            {
                Reddit client = RedditService.GetClient();
                if (Link.Data.Hidden)
                {
                    await client.LinksAndComments.Unhide(Link.Data.Name);
                }
                else
                {
                    await client.LinksAndComments.Hide(Link.Data.Name);
                }
                Link.Data.Hidden = !Link.Data.Hidden;
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Failed to update link hidden state"));
            }
        }

        /// <summary>
        /// To show thumbnails or not.
        /// Calculated based on user preferences and subreddit context.
        /// </summary>
        /// <returns>To show thumbnails or not</returns>
        protected bool ShowThumbnails()
        {
            MediaPreference? preference = RedditService.Preferences?.Thumbnails;
            if (preference == null)
            {
                preference = MediaPreference.Subreddit;
            }
            return preference switch
            {
                MediaPreference.On => true,
                MediaPreference.Off => false,
                MediaPreference.Subreddit => Subreddit?.Data?.ShowMedia ?? true,
                _ => true,
            };
        }
    }
}
