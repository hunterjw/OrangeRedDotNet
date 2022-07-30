using Blazored.Toast.Services;
using Blazorise;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using System;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages.Settings
{
    /// <summary>
    /// App settings component
    /// </summary>
    public partial class AppSettings
    {
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Settings service
        /// </summary>
        [Inject]
        public SettingsService SettingsService { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// App settings
        /// </summary>
        protected Models.Settings Settings { get; set; }
        /// <summary>
        /// Validations object
        /// </summary>
        protected Validations Validations { get; set; }
        /// <summary>
        /// If the save button is disabled or not
        /// </summary>
        protected bool SaveButtonDisabled { get; set; } = false;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            try
            {
                Settings = SettingsService.Settings;
            }
            catch (Exception)
            {
                ToastService.ShowError("Error loading settings");
            }
        }

        /// <summary>
        /// Handle the valid submission of the edit form
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task HandleValidSubmit()
        {
            try
            {
                SettingsService.SaveSettings(Settings);
                ToastService.ShowSuccess("Settings saved");
                await Validations.ClearAll();
            }
            catch (Exception)
            {
                ToastService.ShowError("Error saving settings");
            }
        }

        /// <summary>
        /// OnValidationsStatusChange event handler
        /// </summary>
        /// <param name="args">Event args</param>
        protected void OnValidationsStatusChange(ValidationsStatusChangedEventArgs args)
        {
            SaveButtonDisabled = args.Status == ValidationStatus.Error;
        }
    }
}
