using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Shared.Multis;
using RedditDotNet.Models.Account;
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
        public Reddit Reddit { get; set; }

        /// <summary>
        /// Injected Modal service
        /// </summary>
        [Inject]
        public IModalService ModalService { get; set; }

        /// <summary>
        /// MultiReddit list
        /// </summary>
        protected List<MultiReddit> MultiReddits { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            MultiReddits = await Reddit.Multis.GetMine(true);
        }

        /// <summary>
        /// OnClick event handler for the New button
        /// </summary>
        protected async void NewMultiRedditButton_OnClick()
        {
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
                Class = "blazored-modal wideModal"
            };
            IModalReference modal = ModalService.Show<EditMultiRedditModal>("New MultiReddit", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                var updateModelResult = result.Data as MultiRedditUpdate;
                Identity identity = await Reddit.Account.GetIdentity();
                string path = $"user/{identity.Name}/m/{updateModelResult.DisplayName.FormatNewMultiName()}";
                MultiReddit newMulti = await Reddit.Multis.CreateMulti(path, updateModelResult, true);
                MultiReddits.Add(newMulti);
                StateHasChanged();
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
