using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using System.Threading.Tasks;
using System;
using System.Linq;
using OrangeRedDotNet.Models.Comments;
using System.Collections.Generic;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// New comment card component
    /// </summary>
    public partial class NewCommentCard
    {
        /// <summary>
        /// Theme service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }
        /// <summary>
        /// Toast service
        /// </summary>
        [Inject]
        public IToastService ToastService { get; set; }
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// Parent thing fullname
        /// </summary>
        [Parameter]
        public string ParentId { get; set; }
        /// <summary>
        /// Event callback for when new comments are created
        /// </summary>
        [Parameter]
        public EventCallback<List<CommentBase>> NewComments { get; set; }

        /// <summary>
        /// Comment parameters
        /// </summary>
        protected CommentParameters CommentParameters { get; set; }
        /// <summary>
        /// If we are sending the comment or not
        /// </summary>
        protected bool Commenting { get; set; } = false;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            RefreshParameters();
        }

        /// <summary>
        /// On click handler for the comment button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task CommentButton_OnClick()
        {
            if (!Commenting)
            {
                try
                {
                    Commenting = true;
                    var response = await RedditService.GetClient().LinksAndComments.Comment(CommentParameters);
                    if (response?.Content?.Errors?.Count > 0)
                    {
                        string message = string.Join(Environment.NewLine, response.Content.Errors.Select(_ => string.Join(" - ", _)));
                        ToastService.ShowError(message, heading: "Error creating comment");
                    }
                    else
                    {
                        if (NewComments.HasDelegate)
                        {
                            await NewComments.InvokeAsync(response.Content.Data.Comments);
                        }
                        RefreshParameters();
                    }
                }
                catch (RedditApiException rex)
                {
                    ToastService.ShowError(rex.MakeErrorMessage("Error creating comment"));
                }
                finally
                {
                    Commenting = false;
                }
            }
        }

        /// <summary>
        /// Refresh the comment parameters
        /// </summary>
        private void RefreshParameters()
        {
            CommentParameters = new()
            {
                ThingId = ParentId
            };
        }
    }
}
