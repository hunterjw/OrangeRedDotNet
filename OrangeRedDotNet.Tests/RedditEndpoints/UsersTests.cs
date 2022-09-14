using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Interfaces;
using OrangeRedDotNet.Models.Account;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Parameters.Users;
using OrangeRedDotNet.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class UsersTests : TestBase
    {
        [TestMethod]
        public async Task GetUsersByIds()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsNotNull(identity);

            Dictionary<string, UserData> users = await client.Users.GetUsersByIds(new()
            {
                Ids = new[] { "t2_" + identity.Id }
            });
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task IsUsernameAvailable()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsNotNull(identity);

            bool usernameAvailable = await client.Users.IsUsernameAvailable(new()
            {
                Username = identity.Name
            });
            Assert.IsFalse(usernameAvailable);
        }

        [TestMethod]
        public async Task GetFriendDetails()
        {
            Reddit client = GetRedditClient();
            UserList friends = await client.Account.GetFriends();
            Assert.IsTrue(friends.Data.Children.Any());

            User friendDetails = await client.Users.GetFriendDetails(friends.Data.Children.First().Name);
            Assert.IsNotNull(friendDetails);
        }

        [TestMethod]
        public async Task UpdateFriend_RemoveFriend()
        {
            Reddit client = GetRedditClient();
            UserList friends = await client.Account.GetFriends();
            Assert.IsNotNull(friends);

            Listing<Link> frontpage = await client.Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsNotNull(frontpage);

            IEnumerable<string> friendNames = friends.Data?.Children?.Select(_ => _.Name);
            string username = frontpage.Data.Children.Where(_ => !friendNames.Contains(_.Data.Author)).First().Data.Author;

            User newFriend = await client.Users.UpdateFriend(new()
            {
                Username = username
            });
            Assert.IsNotNull(newFriend);

            await client.Users.RemoveFriend(username);
        }

        [TestMethod]
        public async Task GetTrophies()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsNotNull(identity);

            TrophyList trophies = await client.Users.GetTrophies(identity.Name);
            Assert.IsNotNull(trophies);
        }

        [TestMethod]
        public async Task GetAbout()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsNotNull(identity);

            Account about = await client.Users.GetAbout(identity.Name);
            Assert.IsNotNull(about);
        }

        [TestMethod]
        public async Task GetListing()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsNotNull(identity);

            Listing<ILinkOrComment> listing = await client.Users.GetListing(identity.Name, UserProfileListingType.Overview);
            Assert.IsNotNull(listing);
        }
    }
}
