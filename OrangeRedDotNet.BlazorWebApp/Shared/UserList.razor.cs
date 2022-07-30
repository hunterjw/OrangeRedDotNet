using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;

namespace OrangeRedDotNet.BlazorWebApp.Shared
{
    public partial class UserList
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        [Parameter]
		public OrangeRedDotNet.Models.Account.UserList Users { get; set; }

		[Parameter]
		public string DateLabel { get; set; }
	}
}
