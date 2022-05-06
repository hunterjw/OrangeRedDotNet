using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Main Nav Menu component
    /// </summary>
    public partial class NavMenu
    {
        /// <summary>
        /// Reddit Service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Configuration
        /// </summary>
        [Inject]
        public IConfiguration Configuration { get; set; }

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
            RedditService.LoginFinished += RefreshComponent;
            RedditService.LogoutFinished += RefreshComponent;
            // ensure identity is loaded on initial load
            await RedditService.LoadIdentity();
        }

        /// <summary>
        /// Toggle the nav menu collapse state
        /// </summary>
        protected void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }


        /// <summary>
        /// Close the nav menu
        /// </summary>
        protected void CloseNavMenu()
        {
            CollapseNavMenu = true;
        }

        /// <summary>
        /// Handler for when to refresh this component
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">Args object</param>
        void RefreshComponent(object sender, object args)
        {
            StateHasChanged();
        }
    }
}
