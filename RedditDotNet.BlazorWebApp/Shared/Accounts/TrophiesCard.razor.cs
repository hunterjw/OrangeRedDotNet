using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
    public partial class TrophiesCard
    {
        [Parameter]
        public TrophyList Trophies { get; set; }

        protected bool TrophiesCardCollapsed { get; set; } = true;

        protected void TrophiesCollapseButton_OnClick()
        {
            TrophiesCardCollapsed = !TrophiesCardCollapsed;
        }
    }
}
