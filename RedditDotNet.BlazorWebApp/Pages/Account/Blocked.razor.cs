using Microsoft.AspNetCore.Components;
using RedditDotNet.Models;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
	public partial class Blocked
	{
		[Inject]
		public Reddit Reddit { get; set; }

		public Thing<UserList> Users { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Users = await Reddit.Account.GetBlocked();
		}
	}
}
