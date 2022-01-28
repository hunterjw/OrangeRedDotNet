using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Main Nav Menu component
    /// </summary>
    public partial class NavMenu
    {
        /// <summary>
        /// Identity Service
        /// </summary>
        [Inject]
        public IdentityService IdentityService { get; set; }

        /// <summary>
        /// Current user identity
        /// </summary>
        protected AccountData Identity { get; set; }

        /// <summary>
        /// To have the nav menu collapsed or not
        /// </summary>
        protected bool CollapseNavMenu = true;

        /// <summary>
        /// Nav bar CSS class
        /// </summary>
        protected string NavBarCssClass => CollapseNavMenu ? null : "show";

        /// <summary>
        /// Nav button css class
        /// </summary>
        protected string NavButtonCssClass => CollapseNavMenu ? "collapsed" : null;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            Identity = await IdentityService.GetIdentity();
        }

        /// <summary>
        /// Toggle the nav menu collapse state
        /// </summary>
        protected void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }

        /// <summary>
        /// Get the URL for the current user profile
        /// </summary>
        /// <returns>User profile URL</returns>
        protected string GetProfileUrl()
        {
            return $"user/{Identity?.Name}";
        }
    }
}
