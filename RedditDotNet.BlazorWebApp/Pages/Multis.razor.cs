using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Shared.Multis;
using RedditDotNet.Exceptions;
using RedditDotNet.Models.Multis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Page for displaying your MultiReddits
    /// </summary>
    public partial class Multis
    {
        /// <summary>
        /// Injected Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Injected Modal service
        /// </summary>
        [Inject]
        public IModalService ModalService { get; set; }
        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }

        /// <summary>
        /// MultiReddit list
        /// </summary>
        protected List<MultiReddit> MultiReddits { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            if (!RedditService.LoggedIn)
            {
                NavigationManager.NavigateTo("");
            }
            try
            {
                MultiReddits = await RedditService.GetClient().Multis.GetMine(true);
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading multireddits"));
            }
        }

        /// <summary>
        /// OnClick event handler for the New button
        /// </summary>
        protected async void NewMultiRedditButton_OnClick()
        {
            if (!RedditService.LoggedIn)
            {
                return;
            }
            var updateModel = new MultiRedditUpdate()
            {
                DescriptionMd = "",
                DisplayName = "",
                Visibility = "private",
                KeyColor = "",
                Subreddits = new List<MultiSubredditUpdate>()
            };
            var parameters = new ModalParameters();
            parameters.Add("UpdateModel", updateModel);
            var options = new ModalOptions
            {
                Class = $"blazored-modal wideModal {(App.DarkMode ? "dark-modal" : "")}"
            };
            IModalReference modal = ModalService.Show<EditMultiRedditModal>("New MultiReddit", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                try
                {
                    var updateModelResult = result.Data as MultiRedditUpdate;
                    string path = $"user/{RedditService.Identity.Name}/m/{updateModelResult.DisplayName.FormatNewMultiName()}";
                    MultiReddit newMulti = await RedditService.GetClient().Multis.CreateMulti(path, updateModelResult, true);
                    MultiReddits.Add(newMulti);
                    StateHasChanged();
                    ToastService.ShowSuccess("Multireddit added");
                }
                catch (RedditApiException rex)
                {
                    ToastService.ShowError(rex.MakeErrorMessage("Error adding multireddit"));
                }
            }
        }

        /// <summary>
        /// Handler for when a multireddit is deleted
        /// </summary>
        /// <param name="path">Path of the deleted multireddit</param>
        public void OnMultiRedditDelete(string path)
        {
            MultiReddits.RemoveAll(_ => _.Data.Path.Equals(path));
            StateHasChanged();
        }

        /// <summary>
        /// Handler for when a multireddit is copied
        /// </summary>
        /// <param name="copiedMultiReddit">The newly created multireddit</param>
        public void OnMultiRedditCopy(MultiReddit copiedMultiReddit)
        {
            MultiReddits.Add(copiedMultiReddit);
            StateHasChanged();
        }
    }
}
