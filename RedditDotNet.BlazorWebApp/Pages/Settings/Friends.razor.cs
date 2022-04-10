using Microsoft.AspNetCore.Components;
using RedditDotNet.Exceptions;
using RedditDotNet.Models.Account;
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
        /// Friended users
        /// </summary>
        protected UserList MyFriends { get; set; }
        /// <summary>
        /// New friend username
        /// </summary>
        protected string NewFriend { get; set; } = string.Empty;
        /// <summary>
        /// If adding a new friend was successful
        /// </summary>
        protected bool? NewFriendSuccess { get; set; }
        /// <summary>
        /// New friend error string
        /// </summary>
        protected string NewFriendError { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            MyFriends = await RedditService.GetClient().Account.GetFriends();
        }

        /// <summary>
        /// Handle the submission of a new friend
        /// </summary>
        /// <returns>Awaitable task</returns>
        protected async Task HandleValidNewFriendSubmit()
        {
            NewFriendSuccess = null;
            NewFriendError = null;
            if (!string.IsNullOrWhiteSpace(NewFriend))
            {
                try
                {
                    var newFriend = await RedditService.GetClient().Users.UpdateFriend(NewFriend);
                    MyFriends.Data.Children.Add(newFriend);
                    NewFriend = string.Empty;
                    NewFriendSuccess = true;
                }
                catch (RedditApiException rex)
                {
                    NewFriendError = $"Error adding new friend";
                    if (!string.IsNullOrWhiteSpace(rex.ResponseContent?.Explanation))
                    {
                        NewFriendError += $": {rex.ResponseContent?.Explanation}";
                    }
                    NewFriendSuccess = false;
                }
            }
        }
    }
}
