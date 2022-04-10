using Microsoft.AspNetCore.Components;
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
        /// Current user preferences
        /// </summary>
        protected Preferences Preferences { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            Preferences = await RedditService.GetClient().Account.GetPreferences();
        }

        /// <summary>
        /// Handle the valid submission of the edit form
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task HandleValidSubmit()
        {
            Preferences = await RedditService.GetClient().Account.SetPreferences(Preferences);
        }
    }
}
