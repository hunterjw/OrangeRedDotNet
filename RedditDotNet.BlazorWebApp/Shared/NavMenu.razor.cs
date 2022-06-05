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
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// If the nav menu is collapsed or not
        /// </summary>
        protected bool NavMenuCollapsed { get; set; } = false;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            App.ThemeChanged += RefreshComponent;
            RedditService.LoginFinished += RefreshComponent;
            RedditService.LogoutFinished += RefreshComponent;
            // ensure identity is loaded on initial load
            await RedditService.LoadIdentity();
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

        /// <summary>
        /// OnClick event handler for the login button
        /// </summary>
        void LoginButtonOnClick()
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
