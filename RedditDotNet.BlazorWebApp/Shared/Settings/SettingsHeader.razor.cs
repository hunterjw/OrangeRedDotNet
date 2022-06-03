using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared.Settings
{
    /// <summary>
    /// Nav tab options for the settings page(s)
    /// </summary>
    public enum SettingsNavTab
    {
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
