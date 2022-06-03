using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using RedditDotNet.Exceptions;
using RedditDotNet.Models.Account;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages.Settings
{
    /// <summary>
    /// Page for friended users
    /// </summary>
    public partial class Friends
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
        /// Friended users
        /// </summary>
        protected UserList MyFriends { get; set; }
        /// <summary>
        /// New friend username
        /// </summary>
        protected string NewFriend { get; set; } = string.Empty;

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                MyFriends = await RedditService.GetClient().Account.GetFriends();
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error loading friends"));
            }
        }

        /// <summary>
        /// On click handler for the new friend button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task NewFriendButtonOnClick()
        {
            if (!string.IsNullOrWhiteSpace(NewFriend))
            {
                try
                {
                    var newFriend = await RedditService.GetClient().Users.UpdateFriend(NewFriend);
                    if (MyFriends?.Data?.Children?.Count(_ => _.Name.Equals(newFriend.Name)) < 1)
                    {
                        MyFriends.Data.Children.Add(newFriend);
                        ToastService.ShowSuccess("Friend added");
                    }
                    NewFriend = string.Empty;
                }
                catch (RedditApiException rex)
                {
                    ToastService.ShowError(rex.MakeErrorMessage("Error adding new friend"));
                }
            }
        }
    }
}
