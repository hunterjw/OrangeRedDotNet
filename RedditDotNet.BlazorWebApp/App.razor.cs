using Blazorise;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using System;

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
        private Theme theme = new Theme
        {
            //ColorOptions = new ThemeColorOptions
            //{
            //    Primary = "#ff4500"
            //}
        };

        public static bool DarkMode;
        public static Background Background;
        public static TextColor TextColor;
        public static ThemeContrast ThemeContrast;
        public static Color DefaultButtonColor;
        public static EventHandler<object> ThemeChanged;

        /// <summary>
        /// Settings service
        /// </summary>
        [Inject]
        public SettingsService SettingsService { get; set; }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            UpdateColors();
            SettingsService.SettingsChanged += RefreshComponent;
        }

        /// <summary>
        /// Handler for when to refresh this component
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Args object</param>
        protected void RefreshComponent(object sender, object args)
        {
            UpdateColors();
            StateHasChanged();
            ThemeChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Update the app colors
        /// </summary>
        protected void UpdateColors()
        {
            if (SettingsService.Settings.DarkMode)
            {
                DarkMode = true;
                Background = Background.Dark;
                TextColor = TextColor.Light; 
                ThemeContrast = ThemeContrast.Dark;
                DefaultButtonColor = Color.Dark;
            }
            else
            {
                DarkMode = false;
                Background = Background.Light;
                TextColor = TextColor.Dark; 
                ThemeContrast = ThemeContrast.Light;
                DefaultButtonColor = Color.Light;
            }
        }
    }
}
