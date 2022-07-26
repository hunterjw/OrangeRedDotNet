using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared
{
    /// <summary>
    /// Timescale select dropdown
    /// </summary>
    public partial class TimescaleSelect
    {
        /// <summary>
        /// Current value
        /// </summary>
        [Parameter]
        public string Timescale { get; set; }

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
