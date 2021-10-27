using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
	public partial class KarmaBreakdown
	{
		[Inject]
		protected Reddit Reddit { get; set; }

		protected List<SubredditKarmaBreakdown> AccountKarmaBreakdown { get; set; }
		protected bool RawDataCollapsed { get; set; } = true;

		protected override async Task OnInitializedAsync()
		{
			AccountKarmaBreakdown = await Reddit.Account.GetKarmaBreakdown();
		}

		protected void RawDataButton_OnClick(MouseEventArgs e)
		{
			RawDataCollapsed = !RawDataCollapsed;
		}
	}
}
