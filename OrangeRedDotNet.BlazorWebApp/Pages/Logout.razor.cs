using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Logout page
    /// </summary>
    public partial class Logout
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await RedditService.Logout();
            NavigationManager.NavigateTo("");
        }
    }
}
