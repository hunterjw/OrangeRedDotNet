using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Collections.Generic;

namespace RedditDotNet.BlazorWebApp.Shared
{
	public partial class UserList
	{
		[Parameter]
		public List<User> Users { get; set; }

		[Parameter]
		public string DateLabel { get; set; }
	}
}
