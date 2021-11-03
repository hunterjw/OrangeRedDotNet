using Microsoft.AspNetCore.Components;
using RedditDotNet.Models;

namespace RedditDotNet.BlazorWebApp.Shared
{
	public partial class UserList
	{
		[Parameter]
		public Thing<Models.Account.UserList> Users { get; set; }

		[Parameter]
		public string DateLabel { get; set; }
	}
}
