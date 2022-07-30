using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Account;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages.Settings
{
    /// <summary>
    /// Page for blocked and trusted users
    /// </summary>
    public partial class Blocked
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
        /// Blocked users
        /// </summary>
        public UserList MyBlocked { get; set; }
        /// <summary>
        /// Trusted users
        /// </summary>
        public UserList MyTrusted { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                MyBlocked = await RedditService.GetClient().Account.GetBlocked();
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading blocked users"));
            }
            try
            {
                MyTrusted = await RedditService.GetClient().Account.GetTrusted();
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading trusted users"));
            }
        }
    }
}
