using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using System;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Search
{
    /// <summary>
    /// Select dropdown for search sort
    /// </summary>
    public partial class SearchSortSelect
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Current value
        /// </summary>
        [Parameter]
        public string SearchSort { get; set; }

        /// <summary>
        /// Executed when the value is changed
        /// </summary>
        [Parameter]
        public Func<ChangeEventArgs, Task> OnChange { get; set; }

        /// <summary>
        /// On change handler for the select
        /// </summary>
        /// <param name="args">Args</param>
        /// <returns>Awaitable task</returns>
        protected async Task Select_OnChange(ChangeEventArgs args)
        {
            await OnChange(args);
        }
    }
}
