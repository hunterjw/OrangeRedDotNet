using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Page for displaying user profiles
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }

        /// <summary>
        /// Identity service
        /// </summary>
        [Inject]
        public IdentityService IdentityService { get; set; }

        /// <summary>
        /// Account username
        /// </summary>
        [Parameter]
        public string UserName { get; set; }

        /// <summary>
        /// Account 
        /// </summary>
        protected Models.Account.Account Account { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            Account = await Reddit.Users.GetAbout(UserName);
        }
    }
}
