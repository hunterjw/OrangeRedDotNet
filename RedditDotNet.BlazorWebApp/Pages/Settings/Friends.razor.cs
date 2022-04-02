using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Settings
{
    /// <summary>
    /// Page for friended users
    /// </summary>
    public partial class Friends
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// Friended users
        /// </summary>
        protected UserList MyFriends { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            MyFriends = await RedditService.GetClient().Account.GetFriends();
        }
    }
}
