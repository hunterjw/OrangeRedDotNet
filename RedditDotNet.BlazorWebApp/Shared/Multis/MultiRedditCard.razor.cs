using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.BlazorWebApp.Services;
using RedditDotNet.Exceptions;
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
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }

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
        /// OnClick event handler for the Remove Subreddit button
        /// </summary>
        /// <param name="subreddit">Name of the Subreddit to remove</param>
        protected async void RemoveSubredditButton_OnClick(string subreddit)
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
                var parameters = new ModalParameters();
                parameters.Add("MultiRedditName", MultiReddit.Data.DisplayName);
                parameters.Add("SubredditName", subreddit);
                var options = new ModalOptions
                {
                    HideHeader = true,
                    Class = $"blazored-modal {(App.DarkMode ? "dark-modal" : "")}"
                };
                IModalReference modal = ModalService.Show<RemoveMultiSubredditModal>("", parameters, options);
                ModalResult result = await modal.Result;
                if (!result.Cancelled)
                {
                    await RedditService.GetClient().Multis.DeleteSubreddit(MultiReddit.Data.Path, subreddit);
                    MultiReddit.Data.Subreddits.RemoveAll(_ => _.Name.Equals(subreddit));
                    StateHasChanged();
                    ToastService.ShowSuccess($"{subreddit} removed from {MultiReddit.Data.DisplayName}");
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error removing subreddit"));
            }
        }

        /// <summary>
        /// OnClick event handler for the Add Subreddit button
        /// </summary>
        protected async void AddSubredditButton_OnClick()
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
                var reddit = RedditService.GetClient();
                if (!string.IsNullOrWhiteSpace(AddSubredditName)
                    && !MultiReddit.Data.Subreddits.Any(_ =>
                        _.Name.Equals(AddSubredditName, StringComparison.OrdinalIgnoreCase)))
                {
                    MultiSubreddit result = await reddit.Multis.AddSubreddit(MultiReddit.Data.Path, AddSubredditName);
                    MultiReddit.Data.Subreddits.Add(result);
                    StateHasChanged();
                    ToastService.ShowSuccess($"{AddSubredditName} added to {MultiReddit.Data.DisplayName}");

                    MultiReddit = await reddit.Multis.GetMulti(MultiReddit.Data.Path, true);
                    AddSubredditName = string.Empty;
                    StateHasChanged();
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error adding subreddit"));
            }
        }

        /// <summary>
        /// OnClick event handler for the delete button
        /// </summary>
        protected async void DeleteMultiRedditButton_OnClick()
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
                var parameters = new ModalParameters();
                parameters.Add("MultiRedditName", MultiReddit.Data.DisplayName);
                var options = new ModalOptions
                {
                    HideHeader = true,
                    Class = $"blazored-modal {(App.DarkMode ? "dark-modal" : "")}"
                };
                IModalReference modal = ModalService.Show<DeleteMultiRedditModal>("", parameters, options);
                ModalResult result = await modal.Result;
                if (!result.Cancelled)
                {
                    await RedditService.GetClient().Multis.DeleteMulti(MultiReddit.Data.Path);
                    OnMultiRedditDelete(MultiReddit.Data.Path);
                    ToastService.ShowSuccess($"{MultiReddit.Data.DisplayName} deleted");
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error deleting multireddit"));
            }
        }

        /// <summary>
        /// On click event handler for the edit button
        /// </summary>
        protected async void EditMultiRedditButton_OnClick()
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
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
                    Class = $"blazored-modal wideModal {(App.DarkMode ? "dark-modal" : "")}"
                };
                IModalReference modal = ModalService.Show<EditMultiRedditModal>("Edit MultiReddit", parameters, options);
                ModalResult result = await modal.Result;
                if (!result.Cancelled)
                {
                    var updateModelResult = result.Data as MultiRedditUpdate;
                    MultiReddit = await RedditService.GetClient().Multis.UpdateMulti(MultiReddit.Data.Path, updateModelResult, true);
                    StateHasChanged();
                    ToastService.ShowSuccess($"{MultiReddit.Data.DisplayName} updated");
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error updating multireddit"));
            }
        }

        /// <summary>
        /// OnClick event handler for the copy button
        /// </summary>
        protected async void CopyMultiRedditButton_OnClick()
        {
            try
            {
                if (!RedditService.LoggedIn)
                {
                    return;
                }
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
                    Class = $"blazored-modal wideModal {(App.DarkMode ? "dark-modal" : "")}"
                };
                IModalReference modal = ModalService.Show<EditMultiRedditModal>("Copy MultiReddit", parameters, options);
                ModalResult result = await modal.Result;
                if (!result.Cancelled)
                {
                    var updateModelResult = result.Data as MultiRedditUpdate;
                    string newPath = $"user/{RedditService.Identity.Name}/m/{updateModelResult.DisplayName.FormatNewMultiName()}";
                    var copiedMultiReddit = await RedditService.GetClient().Multis.CopyMulti(
                        MultiReddit.Data.Path,
                        newPath,
                        updateModelResult.DisplayName,
                        updateModelResult.DescriptionMd,
                        true);
                    OnMultiRedditCopy(copiedMultiReddit);
                    ToastService.ShowSuccess($"{MultiReddit.Data.DisplayName} copied");
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error copying multireddit"));
            }
        }
    }
}
