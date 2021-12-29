using Microsoft.AspNetCore.Components;
using RedditDotNet.Models;
using System;
using System.Collections.Generic;

namespace RedditDotNet.BlazorWebApp.Shared
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
