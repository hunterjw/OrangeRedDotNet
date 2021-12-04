using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Models.Links;
using System.Linq;
using System.Web;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Component to display a link as a card
    /// </summary>
    public partial class LinkCard
    {
        /// <summary>
        /// Link to display
        /// </summary>
        [Parameter]
        public Link Link { get; set; }

        /// <summary>
        /// If the content of the Link is collapsed or not
        /// </summary>
        protected bool ContentCollapsed { get; set; } = true;

        /// <summary>
        /// Get a preview image URL
        /// </summary>
        protected string PreviewUrl
        {
            get
            {
                if (Link?.Data?.Preview?.Images?.Count > 0)
                {
                    return HttpUtility.HtmlDecode(Link.Data.Preview.Images.First().Resolutions.First().Url);
                }
                // TODO replace this with locally hosted resource
                return "https://via.placeholder.com/256";
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
    }
}
