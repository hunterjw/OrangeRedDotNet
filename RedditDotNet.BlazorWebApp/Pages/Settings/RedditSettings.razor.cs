using Blazored.Toast.Services;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Exceptions;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Settings
{
    /// <summary>
    /// Page for displaying Reddit settings
    /// </summary>
    public partial class RedditSettings
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Current user preferences
        /// </summary>
        protected Preferences Preferences { get; set; }
        /// <summary>
        /// Validations object
        /// </summary>
        protected Validations Validations { get; set; }
        /// <summary>
        /// If the save button is disabled or not
        /// </summary>
        protected bool SaveButtonDisabled { get; set; } = false;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Preferences = await RedditService.GetClient().Account.GetPreferences();
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading preferences"));
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
                Preferences = await RedditService.GetClient().Account.SetPreferences(Preferences);
                ToastService.ShowSuccess("Preferences saved");
                await Validations.ClearAll();
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error saving preferences"));
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
