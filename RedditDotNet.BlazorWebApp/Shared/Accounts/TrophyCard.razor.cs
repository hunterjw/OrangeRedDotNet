using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
    /// <summary>
    /// Trophy card
    /// </summary>
    public partial class TrophyCard
    {
        /// <summary>
        /// Award data
        /// </summary>
        [Parameter]
        public AwardData AwardData { get; set; }
    }
}
