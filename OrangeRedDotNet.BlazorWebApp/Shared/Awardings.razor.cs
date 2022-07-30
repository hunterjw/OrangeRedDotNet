using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models;
using System.Collections.Generic;

namespace OrangeRedDotNet.BlazorWebApp.Shared
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
