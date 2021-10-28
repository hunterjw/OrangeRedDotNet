using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Account
{
	public partial class KarmaBreakdown
	{
        private static readonly SHA1 MySha1 = SHA1.Create();

		[Inject]
		protected Reddit Reddit { get; set; }

		protected List<SubredditKarmaBreakdown> AccountKarmaBreakdown { get; set; }
		protected bool RawDataCollapsed { get; set; } = true;
		protected IEnumerable<string> SubredditNameHashColors { get; set; }

		protected override async Task OnInitializedAsync()
		{
			AccountKarmaBreakdown = await Reddit.Account.GetKarmaBreakdown();
			SubredditNameHashColors = GetSubredditNameHashColors(AccountKarmaBreakdown.Select(_ => _.Subreddit));
		}

		protected void RawDataButton_OnClick(MouseEventArgs e)
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
