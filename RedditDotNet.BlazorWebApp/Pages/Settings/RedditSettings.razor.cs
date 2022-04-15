using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
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
        /// Current user preferences
        /// </summary>
        protected Preferences Preferences { get; set; }

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
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error saving preferences"));
            }
        }
    }
}
