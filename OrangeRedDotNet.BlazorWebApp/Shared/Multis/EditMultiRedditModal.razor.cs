using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Multis;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Multis
{
    public partial class EditMultiRedditModal
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
        /// Validations reference
        /// </summary>
        protected Validations Validations { get; set; }
        /// <summary>
        /// If the save button is disabled or not
        /// </summary>
        protected bool SaveButtonDisabled { get; set; } = false;

        /// <summary>
        /// OnClick event handler for the Cancel button
        /// </summary>
        protected void CancelButton_OnClick()
        {
            ModalInstance.CancelAsync();
        }

        /// <summary>
        /// Handler for the Save button
        /// </summary>
        protected async Task HandleValidSubmit()
        {
            await ModalInstance.CloseAsync(ModalResult.Ok(UpdateModel));
        }

        /// <summary>
        /// OnValidationsStatusChange event handler
        /// </summary>
        /// <param name="args">Event args</param>
        protected void OnValidationsStatusChange(ValidationsStatusChangedEventArgs args)
        {
            SaveButtonDisabled = args.Status == ValidationStatus.Error;
        }
    }
}
