using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Links;
using System.Collections.Generic;

namespace RedditDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Gallery component
    /// </summary>
    public partial class Gallery
    {
        /// <summary>
        /// Link ID
        /// </summary>
        [Parameter]
        public string LinkId { get; set; }

        /// <summary>
        /// Gallery data
        /// </summary>
        [Parameter]
        public GalleryData GalleryData { get; set; }

        /// <summary>
        /// Media metadata
        /// </summary>
        [Parameter] 
        public Dictionary<string, MediaMetadata> MediaMetadata { get; set; }

        /// <summary>
        /// Gets the ID for the gallery carousel
        /// </summary>
        /// <returns>ID string</returns>
        protected string GetCarouselId() => $"{LinkId}-carousel";
    }
}
