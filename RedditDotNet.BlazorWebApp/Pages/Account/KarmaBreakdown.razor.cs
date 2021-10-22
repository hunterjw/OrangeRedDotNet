using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RedditDotNet.Controllers;
using RedditDotNet.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
	public partial class KarmaBreakdown
	{
		[Inject]
		protected Reddit Reddit { get; set; }

		protected List<SubredditKarmaBreakdown> AccountKarmaBreakdown { get; set; }

		protected override async Task OnInitializedAsync()
		{
			AccountKarmaBreakdown = await Reddit.Account.GetKarmaBreakdown();
		}
	}
}
