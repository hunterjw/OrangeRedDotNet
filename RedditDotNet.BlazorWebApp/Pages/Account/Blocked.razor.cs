using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
	public partial class Blocked
	{
		[Inject]
		public Reddit Reddit { get; set; }

		public List<User> Users { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Users = await Reddit.Account.GetBlocked();
		}
	}
}
