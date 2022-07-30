using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Account;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Accounts
{
    /// <summary>
    /// Trophy card
    /// </summary>
    public partial class TrophyCard
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Award data
        /// </summary>
        [Parameter]
        public AwardData AwardData { get; set; }
    }
}
