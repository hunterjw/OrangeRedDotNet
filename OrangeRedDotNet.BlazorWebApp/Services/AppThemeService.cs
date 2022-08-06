using Blazorise;
using OrangeRedDotNet.BlazorWebApp.Models;
using System;

namespace OrangeRedDotNet.BlazorWebApp.Services
{
    /// <summary>
    /// Service for managing the current app theme
    /// </summary>
    public class AppThemeService
    {
        /// <summary>
        /// Current app theme
        /// </summary>
        public AppTheme AppTheme { get { return _appTheme; } }
        private AppTheme _appTheme;

        /// <summary>
        /// Theme changed event handler
        /// </summary>
        public event EventHandler<AppTheme> ThemeChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingsService">Settings service instance</param>
        public AppThemeService(SettingsService settingsService)
        {
            settingsService.SettingsChanged += SettingsChangedHandler;
            UpdateTheme(settingsService.Settings);
        }

        /// <summary>
        /// Event handler for when the settings change
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="newSettings">New settings</param>
        private void SettingsChangedHandler(object sender, Settings newSettings)
        {
            UpdateTheme(newSettings);
        }

        /// <summary>
        /// Update the current theme
        /// </summary>
        /// <param name="settings">Settings to use to update the theme</param>
        private void UpdateTheme(Settings settings)
        {
            AppTheme newTheme = new()
            {
                BlazoriseTheme = new Theme
                {
                    ColorOptions = new ThemeColorOptions
                    {
                        Dark = settings.TrueDark ? "#000000" : "#212529",
                        Primary = "#0d6efd",
                    },
                    BackgroundOptions = new ThemeBackgroundOptions
                    {
                        Dark = settings.TrueDark ? "#000000" : "#212529",
                        Primary = "#0d6efd",
                    },
                },
                DarkMode = settings.DarkMode,
                Background = settings.DarkMode ? Background.Dark : Background.Light,
                SpacingRelated = Margin.Is2.FromBottom,
                SpacingSame = Margin.Is1.FromBottom,
                SpacingSeparate = Margin.Is3.FromBottom,
                TextColor = settings.DarkMode ? TextColor.Light : TextColor.Dark,
                ThemeContrast = settings.DarkMode ? ThemeContrast.Dark : ThemeContrast.Light,
                Border = Border.Secondary,
                DefaultButtonColor = settings.DarkMode ? Color.Dark : Color.Light
            };
            _appTheme = newTheme;
            ThemeChanged?.Invoke(this, newTheme);
        }
    }
}
