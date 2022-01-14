using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Multis;

namespace RedditDotNet.BlazorWebApp.Shared.Multis
{
    public partial class EditMultiRedditModal
    {
        /// <summary>
        /// Modal instance
        /// </summary>
        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }

        /// <summary>
        /// MultiReddit update model
        /// </summary>
        [Parameter]
        public MultiRedditUpdate UpdateModel { get; set; }

        /// <summary>
        /// To hide the Visibility dropdown or not
        /// </summary>
        [Parameter]
        public bool HideVisibility { get; set; } = false;

        /// <summary>
        /// OnClick event handler for the Cancel button
        /// </summary>
        protected void CancelButton_OnClick()
        {
            ModalInstance.CancelAsync();
        }

        /// <summary>
        /// OnClick event handler for the Save button
        /// </summary>
        protected void SaveButton_OnClick()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(UpdateModel));
        }
    }
}
