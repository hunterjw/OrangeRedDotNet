using Blazorise.Charts;
using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
	/// <summary>
	/// Karma breakdown card
	/// </summary>
    public partial class KarmaBreakdownCard
	{
		/// <summary>
		/// Shared SHA1
		/// </summary>
		private static readonly SHA1 MySha1 = SHA1.Create();
		/// <summary>
		/// Pie chart options
		/// </summary>
		private static readonly PieChartOptions ChartOptions = new()
		{
			AspectRatio = 3
		};

		/// <summary>
		/// Get colors for subreddit names based on hash value of subreddit name
		/// </summary>
		/// <param name="subredditNames">Subreddit names</param>
		/// <returns>Colors</returns>
		private static List<string> GetSubredditNameHashColors(IEnumerable<string> subredditNames)
		{
			IEnumerable<byte[]> hashes = subredditNames.Select(_ => MySha1.ComputeHash(Encoding.UTF8.GetBytes(_)));
			return hashes.Select(_ => ChartColor.FromRgba(_[0], _[1], _[2], 1).ToJsRgba()).ToList();
		}

		/// <summary>
		/// Account karma breakdown
		/// </summary>
		[Parameter]
		public  KarmaBreakdown AccountKarmaBreakdown { get; set; }

		/// <summary>
		/// If the card is collapsed or not
		/// </summary>
		protected bool CardCollapsed { get; set; } = true;
		/// <summary>
		/// If the raw data is collapsed or not
		/// </summary>
		protected bool RawDataCollapsed { get; set; } = true;
		/// <summary>
		/// Colors for the subreddit names
		/// </summary>
		protected List<string> SubredditNameHashColors { get; set; }
		/// <summary>
		/// Link karma chart
		/// </summary>
		protected PieChart<int> LinkKarmaPieChart;
		/// <summary>
		/// Comment karma
		/// </summary>
		protected PieChart<int> CommentKarmaPieChart;

		/// <inheritdoc/>
		protected override void OnParametersSet()
		{
			SubredditNameHashColors = GetSubredditNameHashColors(AccountKarmaBreakdown.Data.Select(_ => _.Subreddit));
		}

		/// <inheritdoc/>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await LinkKarmaPieChart.AddLabelsDatasetsAndUpdate(
					AccountKarmaBreakdown.Data.Select(_ => _.Subreddit).ToList(),
					new PieChartDataset<int>()
					{
						Label = $"Link Karma",
						Data = AccountKarmaBreakdown.Data.Select(_ => _.LinkKarma).ToList(),
						BackgroundColor = SubredditNameHashColors
					});
				await CommentKarmaPieChart.AddLabelsDatasetsAndUpdate(
					AccountKarmaBreakdown.Data.Select(_ => _.Subreddit).ToList(),
					new PieChartDataset<int>()
					{
						Label = $"Comment Karma",
						Data = AccountKarmaBreakdown.Data.Select(_ => _.CommentKarma).ToList(),
						BackgroundColor = SubredditNameHashColors
					});
			}
		}
	}
}
