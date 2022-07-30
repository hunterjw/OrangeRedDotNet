using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Multis
{
    /// <summary>
    /// Modal to remove a Subreddit from a MultiReddit
    /// </summary>
    public partial class RemoveMultiSubredditModal
    {
        /// <summary>
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

        /// <summary>
        /// Modal instance
        /// </summary>
        [CascadingParameter] 
        BlazoredModalInstance ModalInstance { get; set; }

        /// <summary>
        /// Name of the MultiReddit
        /// </summary>
        [Parameter]
        public string MultiRedditName { get; set; }
        /// <summary>
        /// Subreddit Name
        /// </summary>
        [Parameter]
        public string SubredditName { get; set; }

        /// <summary>
        /// OnClick event handler for the Cancel button
        /// </summary>
        protected void CancelButton_OnClick()
        {
            ModalInstance.CancelAsync();
        }

        /// <summary>
        /// OnClick event handler for the Remove button
        /// </summary>
        protected void RemoveButton_OnClick()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(""));
        }
    }
}
