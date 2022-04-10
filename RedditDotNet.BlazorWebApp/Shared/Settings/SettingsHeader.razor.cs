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
        /// Active settings tab
        /// </summary>
        [Parameter]
        public SettingsNavTab ActiveTab { get; set; }
    }
}
