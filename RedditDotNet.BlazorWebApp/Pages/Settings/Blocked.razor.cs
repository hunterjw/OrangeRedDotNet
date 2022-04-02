using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Settings
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
            MyBlocked = await RedditService.GetClient().Account.GetBlocked();
            MyTrusted = await RedditService.GetClient().Account.GetTrusted();
        }
    }
}
