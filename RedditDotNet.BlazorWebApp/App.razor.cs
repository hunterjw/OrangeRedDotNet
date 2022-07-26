using Blazorise;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Models;
using RedditDotNet.BlazorWebApp.Services;

namespace RedditDotNet.BlazorWebApp
{
    /// <summary>
    /// Main app component
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// App theme
        /// </summary>
        private readonly Theme theme = new()
        {
            //ColorOptions = new ThemeColorOptions
            //{
            //    Primary = "#ff4500"
            //}
        };

        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            ThemeService.ThemeChanged += OnThemeChanged;
        }

        /// <summary>
        /// On theme changed event handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="newTheme">New theme</param>
        protected void OnThemeChanged(object sender, AppTheme newTheme)
        {
            StateHasChanged();
        }
    }
}
