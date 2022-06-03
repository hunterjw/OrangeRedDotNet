using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Parameters;

namespace RedditDotNet.BlazorWebApp.Shared.Users
{
    /// <summary>
    /// Nav tab component for the user profile page
    /// </summary>
    public partial class UserProfileNavTabs
    {
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Profile user name
        /// </summary>
        [Parameter]
        public string UserName { get; set; }
        /// <summary>
        /// The type of the active tab
        /// </summary>
        [Parameter]
        public UserProfileListingType ActiveTab { get; set; }
        /// <summary>
        /// If the user is self
        /// </summary>
        [Parameter]
        public bool IsSelf { get; set; }

        /// <summary>
        /// Navigate to the selected tab
        /// </summary>
        /// <param name="navigateTo">Tab to navigate to</param>
        protected void NavigateTo(UserProfileListingType navigateTo)
        {
            switch (navigateTo)
            {
                case UserProfileListingType.Overview:
                    NavigationManager.NavigateTo($"/user/{UserName}/overview");
                    break;
                case UserProfileListingType.Comments:
                    NavigationManager.NavigateTo($"/user/{UserName}/comments");
                    break;
                case UserProfileListingType.Submitted:
                    NavigationManager.NavigateTo($"/user/{UserName}/submitted");
                    break;
                case UserProfileListingType.Gilded:
                    NavigationManager.NavigateTo($"/user/{UserName}/gilded");
                    break;
                case UserProfileListingType.Upvoted:
                    NavigationManager.NavigateTo($"/user/{UserName}/upvoted");
                    break;
                case UserProfileListingType.Downvoted:
                    NavigationManager.NavigateTo($"/user/{UserName}/downvoted");
                    break;
                case UserProfileListingType.Hidden:
                    NavigationManager.NavigateTo($"/user/{UserName}/hidden");
                    break;
                case UserProfileListingType.Saved:
                    NavigationManager.NavigateTo($"/user/{UserName}/saved");
                    break;
            }
        }
    }
}
