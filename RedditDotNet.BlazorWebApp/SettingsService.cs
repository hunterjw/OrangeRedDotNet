using Blazored.LocalStorage;
using System;

namespace RedditDotNet.BlazorWebApp
{
    /// <summary>
    /// Settings service
    /// </summary>
    public class SettingsService
    {
        /// <summary>
        /// Local storage service
        /// </summary>
        private readonly ISyncLocalStorageService _localStorageService;

        /// <summary>
        /// Current settings
        /// </summary>
        public Settings Settings { get => _settings; }
        private Settings _settings;

        /// <summary>
        /// Settings changed event handler
        /// </summary>
        public event EventHandler<Settings> SettingsChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localStorageService">Local storage service</param>
        public SettingsService(ISyncLocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _settings = LoadSettings();
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <returns>Loaded settings</returns>
        internal Settings LoadSettings()
        {
            var settings = _localStorageService.GetItem<Settings>(Settings.LocalStorageKey);
            if (settings == default)
            {
                settings = Settings.Default;
            }
            return settings;
        }

        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="newSettings">Settings to save</param>
        public void SaveSettings(Settings newSettings)
        {
            _localStorageService.SetItem(Settings.LocalStorageKey, newSettings);
            _settings = newSettings;
            SettingsChanged?.Invoke(this, newSettings);
        }
    }
}
