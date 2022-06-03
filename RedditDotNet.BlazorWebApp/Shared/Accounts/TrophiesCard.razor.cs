using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
    /// <summary>
    /// Trophies card
    /// </summary>
    public partial class TrophiesCard
    {
        /// <summary>
        /// List of trophies
        /// </summary>
        [Parameter]
        public TrophyList Trophies { get; set; }

        /// <summary>
        /// If the card is collapsed
        /// </summary>
        protected bool CardCollapsed { get; set; } = true;
    }
}
