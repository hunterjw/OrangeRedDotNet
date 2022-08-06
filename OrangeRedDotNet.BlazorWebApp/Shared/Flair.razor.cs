using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.Models;
using OrangeRedDotNet.Models.Links;
using System;
using System.Collections.Generic;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Component for displaying flair
    /// </summary>
    public partial class Flair
    {
        /// <summary>
        /// If the content is NSFW
        /// </summary>
        [Parameter]
        public bool Over18 { get; set; } = false;
        /// <summary>
        /// If the content is OC
        /// </summary>
        [Parameter]
        public bool IsOriginalContent { get; set; } = false;
        /// <summary>
        /// If the content is a spoiler
        /// </summary>
        [Parameter]
        public bool Spoiler { get; set; } = false;
        /// <summary>
        /// If the content is sticked
        /// </summary>
        [Parameter]
        public bool Stickied { get; set; } = false;
        /// <summary>
        /// If the content is locked
        /// </summary>
        [Parameter]
        public bool Locked { get; set; } = false;
        /// <summary>
        /// If the content is archived
        /// </summary>
        [Parameter]
        public bool Archived { get; set; } = false;
        /// <summary>
        /// If the user is the original poster or not
        /// </summary>
        [Parameter]
        public bool IsOriginalPoster { get; set; } = false;
        /// <summary>
        /// If the user is banned
        /// </summary>
        [Parameter]
        public bool Banned { get; set; } = false;
        /// <summary>
        /// If a user is a contributor
        /// </summary>
        [Parameter]
        public bool Contributor { get; set; } = false;
        /// <summary>
        /// If a user is a moderator
        /// </summary>
        [Parameter]
        public bool Moderator { get; set; } = false;
        /// <summary>
        /// If the user is muted
        /// </summary>
        [Parameter]
        public bool Muted { get; set; } = false;
        /// <summary>
        /// If the user is subscribed
        /// </summary>
        [Parameter]
        public bool Subscribed { get; set; } = false;
        /// <summary>
        /// Type of flair
        /// </summary>
        [Parameter]
        public string FlairType { get; set; }
        /// <summary>
        /// Flair text
        /// </summary>
        [Parameter]
        public string FlairText { get; set; }
        /// <summary>
        /// Flair text color
        /// </summary>
        [Parameter]
        public string FlairTextColor { get; set; }
        /// <summary>
        /// Flair background color
        /// </summary>
        [Parameter]
        public string FlairBackgroundColor { get; set; }
        /// <summary>
        /// Flair rich text
        /// </summary>
        [Parameter]
        public List<FlairRichtext> FlairRichtext { get; set; }
        /// <summary>
        /// The type of the Link this flair is attached to
        /// </summary>
        [Parameter]
        public LinkType? LinkType { get; set; } = null;
        /// <summary>
        /// If the thing is saved or not
        /// </summary>
        [Parameter]
        public bool Saved { get; set; } = false;
        /// <summary>
        /// If the thing is hidden or not
        /// </summary>
        [Parameter]
        public bool Hidden { get; set; } = false;
        /// <summary>
        /// If the thing is Controversial or not
        /// </summary>
        [Parameter]
        public bool Controversial { get; set; } = false;

        /// <summary>
        /// Get the css class(es) for the flair badge
        /// </summary>
        /// <returns>css class(es)</returns>
        protected string FlairClass()
        {
            string toReturn = "badge me-1 ";
            if (!string.IsNullOrEmpty(FlairTextColor) &&
                !string.IsNullOrEmpty(FlairBackgroundColor))
            {
                toReturn += FlairTextColor.Equals("light", StringComparison.OrdinalIgnoreCase) ?
                            "text-light" : "text-dark";
            }
            else
            {
                toReturn += "bg-primary";
            }
            return toReturn;
        }

        /// <summary>
        /// Get the flair css style for the flair badge
        /// </summary>
        /// <returns>css style</returns>
        protected string FlairStyle()
        {
            if (!string.IsNullOrEmpty(FlairTextColor) &&
                !string.IsNullOrEmpty(FlairBackgroundColor))
            {
                return $"background-color: {FlairBackgroundColor};";
            }
            return string.Empty;
        }
    }
}
