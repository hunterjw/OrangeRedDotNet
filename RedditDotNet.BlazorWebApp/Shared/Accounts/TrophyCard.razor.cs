using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
{
    public partial class TrophyCard
    {
        [Parameter]
        public AwardData AwardData { get; set; }
    }
}
