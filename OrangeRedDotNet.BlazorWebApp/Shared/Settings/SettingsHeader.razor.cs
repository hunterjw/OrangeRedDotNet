using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Settings
{
    /// <summary>
    /// Nav tab options for the settings page(s)
    /// </summary>
    public enum SettingsNavTab
    {
        AppSettings,
        RedditSettings,
        Friends,
        Blocked
    }

    /// <summary>
    /// Header for settings pages
    /// </summary>
    public partial class SettingsHeader
    {
        /// <summary>
        /// Navigation mangager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Active settings tab
        /// </summary>
        [Parameter]
        public SettingsNavTab ActiveTab { get; set; }

        /// <summary>
        /// Navigate to a different tab
        /// </summary>
        /// <param name="settingsTab">Tabe to navigate to</param>
        protected void NavigateTo(SettingsNavTab settingsTab)
        {
            switch (settingsTab)
            {
                case SettingsNavTab.AppSettings:
                    NavigationManager.NavigateTo("/settings/app");
                    break;
                case SettingsNavTab.RedditSettings:
                    NavigationManager.NavigateTo("/settings/reddit");
                    break;
                case SettingsNavTab.Friends:
                    NavigationManager.NavigateTo("/settings/friends");
                    break;
                case SettingsNavTab.Blocked:
                    NavigationManager.NavigateTo("/settings/blocked");
                    break;
            }
        }
    }
}
