using Microsoft.AspNetCore.Components;
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
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
        /// On click event handler for the login button
        /// </summary>
        protected void LogInButton_OnClick()
        {
            NavigationManager.NavigateTo("login");
        }

        /// <summary>
        /// On click event handler for the logout button
        /// </summary>
        protected void LogOutButton_OnClick()
        {
            NavigationManager.NavigateTo("logout");
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
