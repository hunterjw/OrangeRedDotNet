using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Multis;
using System;
using System.Collections.Generic;

namespace RedditDotNet.BlazorWebApp.Shared.Multis
{
    /// <summary>
    /// Component for displaying a list of MultiReddits
    /// </summary>
    public partial class MultiRedditList
    {
        /// <summary>
        /// MultiReddits to display
        /// </summary>
        [Parameter]
        public List<MultiReddit> MultiReddits { get; set; }
        /// <summary>
        /// Delete handler
        /// </summary>
        [Parameter]
        public Action<string> OnMultiRedditDelete { get; set; }
        /// <summary>
        /// Copy handler
        /// </summary>
        [Parameter]
        public Action<MultiReddit> OnMultiRedditCopy { get; set; }
    }
}
