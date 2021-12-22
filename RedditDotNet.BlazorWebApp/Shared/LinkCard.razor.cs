﻿using Microsoft.AspNetCore.Components;
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
    }
}