using Microsoft.AspNetCore.Components;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Listings;
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

        protected TrophyList Trophies { get; set; }

        protected Listing<ILinkOrComment> Overview { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            Account = await Reddit.Users.GetAbout(UserName);
            Trophies = await Reddit.Users.GetTrophies(Account.Data.Name);
            Overview = await Reddit.Users.GetOverview(Account.Data.Name);
        }
    }
}
