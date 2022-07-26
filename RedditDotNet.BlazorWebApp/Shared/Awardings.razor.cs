using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Models;
using System.Collections.Generic;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Component for displaying Link and Comment awards
    /// </summary>
    public partial class Awardings
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// List of awards
        /// </summary>
        [Parameter]
        public List<Awarding> Data { get; set; }
    }
}
