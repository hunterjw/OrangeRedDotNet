using Microsoft.AspNetCore.Components;

namespace RedditDotNet.BlazorWebApp.Shared
{
    public partial class UserList
	{
		[Parameter]
		public Models.Account.UserList Users { get; set; }

		[Parameter]
		public string DateLabel { get; set; }
	}
}
