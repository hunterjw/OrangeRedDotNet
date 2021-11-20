using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
    public partial class Trophies
	{
		[Inject]
		protected Reddit Reddit { get; set; }

		protected TrophyList Awards { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Awards = await Reddit.Account.GetTrophies();
		}
	}
}
