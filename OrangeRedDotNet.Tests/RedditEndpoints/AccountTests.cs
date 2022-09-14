using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class AccountTests : TestBase
    {
        [TestMethod]
        public async Task GetIdentity()
        {
            var result = await GetRedditClient().Account.GetIdentity();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetKarmaBreakdown()
        {
            var result = await GetRedditClient().Account.GetKarmaBreakdown();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetPreferences()
        {
            var result = await GetRedditClient().Account.GetPreferences();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SetPreferences()
        {
            var client = GetRedditClient();
            // Grab current preferences, send them back
            var result = await client.Account.SetPreferences(new()
            {
                Preferences = await client.Account.GetPreferences()
            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetTrophies()
        {
            var result = await GetRedditClient().Account.GetTrophies();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetFriends()
        {
            var result = await GetRedditClient().Account.GetFriends();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBlocked()
        {
            var result = await GetRedditClient().Account.GetBlocked();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetTrusted()
        {
            var result = await GetRedditClient().Account.GetTrusted();
            Assert.IsNotNull(result);
        }
    }
}
