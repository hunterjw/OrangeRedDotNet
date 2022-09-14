using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Exceptions;
using OrangeRedDotNet.Models.Account;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Pages.Settings
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
        /// Theme Service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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
                    var newFriend = await RedditService.GetClient().Users.UpdateFriend(new()
                    {
                        Username = NewFriend
                    });
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

        /// <summary>
        /// On submit handler for the add friend text box
        /// </summary>
        /// <param name="args">Args</param>
        /// <returns>Awaitable task</returns>
        protected async Task OnAddSubmit(KeyboardEventArgs args)
        {
            if (args.Key.Equals("Enter"))
            {
                await NewFriendButtonOnClick();
            }
        }

        /// <summary>
        /// On click handler for the remove friend button
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task RemoveFriendButtonOnClick(User friend)
        {
            try
            {
                await RedditService.GetClient().Users.RemoveFriend(friend.Name);
                MyFriends.Data.Children.RemoveAll(_ => _.Id == friend.Id);
                ToastService.ShowSuccess("Friend removed");
            }
            catch (RedditApiException rex)
            {
                ToastService.ShowError(rex.MakeErrorMessage("Error removing friend"));
            }
        }
    }
}
