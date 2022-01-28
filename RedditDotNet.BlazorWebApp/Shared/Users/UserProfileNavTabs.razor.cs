using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared.Users
{
    /// <summary>
    /// Nav tab component for the user profile page
    /// </summary>
    public partial class UserProfileNavTabs
    {
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
    }
}
