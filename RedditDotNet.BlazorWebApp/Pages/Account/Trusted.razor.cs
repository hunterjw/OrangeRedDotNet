using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
    public partial class Trusted
	{
		[Inject]
		public RedditService RedditService { get; set; }

		public UserList Users { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Users = await RedditService.GetClient().Account.GetTrusted();
		}
	}
}
