using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Multis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditDotNet.BlazorWebApp.Shared.Multis
{
    /// <summary>
    /// Card component for a MultiReddit
    /// </summary>
    public partial class MultiRedditCard
    {
        /// <summary>
        /// Injected Modal service
        /// </summary>
        [Inject]
        public IModalService ModalService { get; set; }

        /// <summary>
        /// Injected Reddit service
        /// </summary>
        [Inject]
        public Reddit Reddit { get; set; }

        /// <summary>
        /// Identity service
        /// </summary>
        [Inject]
        public IdentityService IdentityService { get; set; }

        /// <summary>
        /// MultiReddit to display
        /// </summary>
        [Parameter]
        public MultiReddit MultiReddit { get; set; }

        /// <summary>
        /// Handler for deleting a multireddit
        /// </summary>
        [Parameter]
        public Action<string> OnMultiRedditDelete { get; set; }

        /// <summary>
        /// Handler for copying a multireddit
        /// </summary>
        [Parameter]
        public Action<MultiReddit> OnMultiRedditCopy { get; set; }

        /// <summary>
        /// If the card content is collapsed or not
        /// </summary>
        protected bool ContentCollapsed { get; set; } = true;

        /// <summary>
        /// Name of a subreddit to add to the MultiReddit, bound to a input field
        /// </summary>
        protected string AddSubredditName { get; set; }

        /// <summary>
        /// OnClick handler for buttons that toggle the collapsable region
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected void ContentCollapsedButton_OnClick()
        {
            ContentCollapsed = !ContentCollapsed;
        }

        /// <summary>
        /// OnClick event handler for the Remove Subreddit button
        /// </summary>
        /// <param name="subreddit">Name of the Subreddit to remove</param>
        protected async void RemoveSubredditButton_OnClick(string subreddit)
        {
            var parameters = new ModalParameters();
            parameters.Add("MultiRedditName", MultiReddit.Data.DisplayName);
            parameters.Add("SubredditName", subreddit);
            var options = new ModalOptions
            {
                HideHeader = true
            };
            IModalReference modal = ModalService.Show<RemoveMultiSubredditModal>("", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                await Reddit.Multis.DeleteSubreddit(MultiReddit.Data.Path, subreddit);
                MultiReddit.Data.Subreddits.RemoveAll(_ => _.Name.Equals(subreddit));
                StateHasChanged();
            }
        }

        /// <summary>
        /// OnClick event handler for the Add Subreddit button
        /// </summary>
        protected async void AddSubredditButton_OnClick()
        {
            // TODO make this more resiliant and add feedback to user when something goes wrong
            if (!string.IsNullOrWhiteSpace(AddSubredditName)
                && !MultiReddit.Data.Subreddits.Any(_ =>
                    _.Name.Equals(AddSubredditName, StringComparison.OrdinalIgnoreCase)))
            {
                MultiSubreddit result = await Reddit.Multis.AddSubreddit(MultiReddit.Data.Path, AddSubredditName);
                MultiReddit.Data.Subreddits.Add(result);
                StateHasChanged();
                MultiReddit = await Reddit.Multis.GetMulti(MultiReddit.Data.Path, true);
                AddSubredditName = string.Empty;
                StateHasChanged();
            }
        }

        /// <summary>
        /// OnClick event handler for the delete button
        /// </summary>
        protected async void DeleteMultiRedditButton_OnClick()
        {
            var parameters = new ModalParameters();
            parameters.Add("MultiRedditName", MultiReddit.Data.DisplayName);
            var options = new ModalOptions
            {
                HideHeader = true
            };
            IModalReference modal = ModalService.Show<DeleteMultiRedditModal>("", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                await Reddit.Multis.DeleteMulti(MultiReddit.Data.Path);
                OnMultiRedditDelete(MultiReddit.Data.Path);
            }
        }

        /// <summary>
        /// On click event handler for the edit button
        /// </summary>
        protected async void EditMultiRedditButton_OnClick()
        {
            var updateModel = new MultiRedditUpdate
            {
                DescriptionMd = MultiReddit.Data.DescriptionMd,
                DisplayName = MultiReddit.Data.DisplayName,
                KeyColor = MultiReddit.Data.KeyColor,
                Subreddits = MultiReddit.Data.Subreddits
                    .Select(_ => new MultiSubredditUpdate
                    {
                        Name = _.Name
                    }).ToList(),
                Visibility = MultiReddit.Data.Visibility,
            };
            var parameters = new ModalParameters();
            parameters.Add("UpdateModel", updateModel);
            var options = new ModalOptions
            {
                Class = "blazored-modal wideModal"
            };
            IModalReference modal = ModalService.Show<EditMultiRedditModal>("Edit MultiReddit", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                var updateModelResult = result.Data as MultiRedditUpdate;
                MultiReddit = await Reddit.Multis.UpdateMulti(MultiReddit.Data.Path, updateModelResult, true);
                StateHasChanged();
            }
        }

        /// <summary>
        /// OnClick event handler for the copy button
        /// </summary>
        protected async void CopyMultiRedditButton_OnClick()
        {
            var updateModel = new MultiRedditUpdate()
            {
                DescriptionMd = "",
                DisplayName = "",
                Visibility = "",
                KeyColor = "",
                Subreddits = new List<MultiSubredditUpdate>()
            };
            var parameters = new ModalParameters();
            parameters.Add("UpdateModel", updateModel);
            parameters.Add("HideVisibility", true);
            var options = new ModalOptions
            {
                Class = "blazored-modal wideModal"
            };
            IModalReference modal = ModalService.Show<EditMultiRedditModal>("Copy MultiReddit", parameters, options);
            ModalResult result = await modal.Result;
            if (!result.Cancelled)
            {
                var updateModelResult = result.Data as MultiRedditUpdate;
                AccountData identity = await IdentityService.GetIdentity();
                string newPath = $"user/{identity.Name}/m/{updateModelResult.DisplayName.FormatNewMultiName()}";
                var copiedMultiReddit = await Reddit.Multis.CopyMulti(
                    MultiReddit.Data.Path, 
                    newPath, 
                    updateModelResult.DisplayName, 
                    updateModelResult.DescriptionMd, 
                    true);
                OnMultiRedditCopy(copiedMultiReddit);
            }
        }
    }
}
