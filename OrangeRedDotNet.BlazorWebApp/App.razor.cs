using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Models;
using OrangeRedDotNet.BlazorWebApp.Services;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp
{
    /// <summary>
    /// Main app component
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            ThemeService.ThemeChanged += OnThemeChanged;
            // ensure identity and preferences are loaded on initial load
            await RedditService.LoadIdentity();
            await RedditService.LoadPreferences();
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
