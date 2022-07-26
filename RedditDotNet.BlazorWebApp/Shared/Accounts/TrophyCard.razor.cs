﻿using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Models.Account;

namespace RedditDotNet.BlazorWebApp.Shared.Accounts
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
