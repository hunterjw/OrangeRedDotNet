using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using OrangeRedDotNet.Models.Subreddits;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class SubredditsTests : TestBase
    {
        [TestMethod]
        public async Task GetAbout()
        {
            Reddit client = GetRedditClient();
            Listing<Subreddit> popular = await client.Subreddits.Get(SubredditsType.Popular);
            Assert.IsNotNull(popular);
            Assert.IsTrue(popular.Data.Children.Any());

            Subreddit about = await client.Subreddits.GetAbout(popular.Data.Children.First().Data.DisplayName);
            Assert.IsNotNull(about);
        }

        [TestMethod]
        public async Task GetRules()
        {
            Reddit client = GetRedditClient();
            Listing<Subreddit> popular = await client.Subreddits.Get(SubredditsType.Popular);
            Assert.IsNotNull(popular);
            Assert.IsTrue(popular.Data.Children.Any());

            RulesResponse rules = await client.Subreddits.GetRules(popular.Data.Children.First().Data.DisplayName);
            Assert.IsNotNull(rules);
        }

        [TestMethod]
        public async Task GetMine()
        {
            Reddit client = GetRedditClient();
            Listing<Subreddit> subscribed = await client.Subreddits.GetMine(MySubredditsType.Subscriber);
            Assert.IsNotNull(subscribed);
        }

        [TestMethod]
        public async Task Get()
        {
            Reddit client = GetRedditClient();
            Listing<Subreddit> popular = await client.Subreddits.Get(SubredditsType.Popular);
            Assert.IsNotNull(popular);
        }

        [TestMethod]
        public async Task Subscribe()
        {
            Reddit client = GetRedditClient();
            Listing<Subreddit> subscribed = await client.Subreddits.GetMine(MySubredditsType.Subscriber);
            Assert.IsNotNull(subscribed);
            Assert.IsTrue(subscribed.Data.Children.Any());

            await client.Subreddits.Subscribe(new()
            {
                SubredditName = subscribed.Data.Children.First().Data.DisplayName,
                Action = SubscribeAction.Subscribe
            });
        }

        [TestMethod]
        public async Task SearchSubreddits()
        {
            Reddit client = GetRedditClient();
            SearchSubredditsResponse response = await client.Subreddits.SearchSubreddits(new()
            {
                Query = "ask"
            });
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetPostRequirements()
        {
            await RunWithTestSubreddit(async (testSubreddit) =>
            {
                Reddit client = GetRedditClient();
                PostRequirements response = await client.Subreddits.GetPostRequirements(testSubreddit);
                Assert.IsNotNull(response);
            });
        }
    }
}
