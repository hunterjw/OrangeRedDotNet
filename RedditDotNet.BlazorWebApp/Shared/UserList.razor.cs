using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;

namespace RedditDotNet.BlazorWebApp.Shared
{
    public partial class UserList
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        [Parameter]
		public RedditDotNet.Models.Account.UserList Users { get; set; }

		[Parameter]
		public string DateLabel { get; set; }
	}
}
