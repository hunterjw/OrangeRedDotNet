using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Multis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Multis
{
    /// <summary>
    /// Modal for selecting which MultiReddits to add a subreddit to
    /// </summary>
    public partial class AddToMultiRedditModal
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
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
        /// Subreddit name
        /// </summary>
        [Parameter]
        public string SubredditName { get; set; }

        /// <summary>
        /// State of selected MultiReddits
        /// </summary>
        protected List<MultiRedditState> MultiRedditStates { get; set; }
        /// <summary>
        /// If the create new multi input group is active or not
        /// </summary>
        protected bool CreateNewActive { get; set; } = false;
        /// <summary>
        /// New MultiReddit name
        /// </summary>
        protected string NewMultiName { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var multiReddits = await RedditService.GetClient().Multis.GetMine();
                MultiRedditStates = new List<MultiRedditState>();
                foreach (var multi in multiReddits)
                {
                    bool alreadyAdded = multi.Data.Subreddits.Any(_ => _.Name.Equals(SubredditName));
                    MultiRedditStates.Add(new MultiRedditState
                    {
                        MultiReddit = multi,
                        OriginalState = alreadyAdded,
                        CurrentState = alreadyAdded
                    });
                }
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error getting multireddits"));
            }
        }

        /// <summary>
        /// On change event handler for MultiReddit select boxes
        /// </summary>
        /// <param name="multiRedditName">MultiReddit name</param>
        protected void MultiRedditSelectInput_OnChange(string multiRedditName)
        {
            var multi = MultiRedditStates.FirstOrDefault(_ => _.MultiReddit.Data.Name.Equals(multiRedditName));
            if (multi != default)
            {
                multi.CurrentState = !multi.CurrentState;
            }
        }

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
            ModalInstance.CloseAsync(ModalResult.Ok(MultiRedditStates));
        }

        /// <summary>
        /// OnClick event handler for Create New Submit button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task CreateNewSubmittButton_OnClick()
        {
            try
            {
                var updateModel = new MultiRedditUpdate()
                {
                    DescriptionMd = "",
                    DisplayName = NewMultiName,
                    Visibility = "private",
                    KeyColor = "",
                    Subreddits = new List<MultiSubredditUpdate>()
                };
                string path = $"user/{RedditService.Identity.Name}/m/{NewMultiName.FormatNewMultiName()}";
                MultiReddit newMulti = await RedditService.GetClient().Multis.CreateMulti(path, updateModel);
                MultiRedditStates.Add(new MultiRedditState
                {
                    MultiReddit = newMulti,
                    OriginalState = false,
                    CurrentState = true,
                });
                StateHasChanged();

                NewMultiName = string.Empty;
                CreateNewActive = !CreateNewActive;
                ToastService.ShowSuccess("Multireddit created");
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error creating multireddit"));
            }
        }
    }
}
