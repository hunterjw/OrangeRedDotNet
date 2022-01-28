using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
    public partial class KarmaBreakdownCard
	{
		private static readonly SHA1 MySha1 = SHA1.Create();

		[Parameter]
		public  KarmaBreakdown AccountKarmaBreakdown { get; set; }

		protected bool CardCollapsed { get; set; } = true;

		protected bool RawDataCollapsed { get; set; } = true;

		protected IEnumerable<string> SubredditNameHashColors { get; set; }

		protected override void OnParametersSet()
		{
			SubredditNameHashColors = GetSubredditNameHashColors(AccountKarmaBreakdown.Data.Select(_ => _.Subreddit));
		}

		protected void CollapseButton_OnClick()
		{
			CardCollapsed = !CardCollapsed;
		}

		protected void RawDataButton_OnClick()
		{
			RawDataCollapsed = !RawDataCollapsed;
		}

		private IEnumerable<string> GetSubredditNameHashColors(IEnumerable<string> subredditNames)
		{
			IEnumerable<byte[]> hashes = subredditNames.Select(_ => MySha1.ComputeHash(Encoding.UTF8.GetBytes(_)));
			IEnumerable<Color> colors = hashes.Select(_ => Color.FromArgb(_[0], _[1], _[2]));
			return colors.Select(_ => $"#{_.R:X2}{_.G:X2}{_.B:X2}");
		}
	}
}
