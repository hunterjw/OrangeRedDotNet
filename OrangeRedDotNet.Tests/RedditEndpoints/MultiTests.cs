using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Models.Account;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Multis;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using OrangeRedDotNet.Models.Subreddits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class MultiTests : TestBase
    {
        [TestMethod]
        public async Task CopyMulti()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit copyResult = await client.Multis.CopyMulti(new()
            {
                From = mine.First().Data.Path,
                To = $"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}"
            });
            Assert.IsNotNull(copyResult);

            await client.Multis.DeleteMulti(copyResult.Data.Path);
        }

        [TestMethod]
        public async Task GetMine()
        {
            List<MultiReddit> result = await GetRedditClient().Multis.GetMine();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetByUsername()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            string username = identity?.Name;
            Assert.IsNotNull(username);

            List<MultiReddit> result = await client.Multis.GetByUsername(username);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetMulti()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit result = await client.Multis.GetMulti(mine.First().Data.Path);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateMulti()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            MultiRedditUpdate updateModel = new()
            {
                DescriptionMd = "Test Multireddit",
                DisplayName = "Test Multireddit",
                KeyColor = "",
                Subreddits = new List<MultiSubredditUpdate>(),
                Visibility = "private"
            };
            MultiReddit result = await client.Multis.CreateMulti($"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}", new()
            {
                Model = updateModel
            });
            Assert.IsNotNull(result);

            await client.Multis.DeleteMulti(result.Data.Path);
        }

        [TestMethod]
        public async Task UpdateMulti()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit copyResult = await client.Multis.CopyMulti(new()
            {
                From = mine.First().Data.Path,
                To = $"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}"
            });
            Assert.IsNotNull(copyResult);

            MultiRedditUpdate updateModel = new()
            {
                DescriptionMd = copyResult.Data.DescriptionMd + " Edited",
                DisplayName = copyResult.Data.DisplayName + " Edited",
                KeyColor = copyResult.Data.KeyColor,
                Subreddits = copyResult.Data.Subreddits
                    .Select(_ => new MultiSubredditUpdate
                    {
                        Name = _.Name
                    }).ToList(),
                Visibility = copyResult.Data.Visibility
            };
            MultiReddit updateResult = await client.Multis.UpdateMulti(copyResult.Data.Path, new()
            {
                Model = updateModel
            });
            Assert.IsNotNull(updateResult);

            await client.Multis.DeleteMulti(updateResult.Data.Path);
        }

        [TestMethod]
        public async Task GetDescription()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiDescription result = await client.Multis.GetDescription(mine.First().Data.Path);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateDescription()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit copyResult = await client.Multis.CopyMulti(new()
            {
                From = mine.First().Data.Path,
                To = $"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}"
            });
            Assert.IsNotNull(copyResult);

            MultiDescription updateDescriptionResult = await client.Multis.UpdateDescription(copyResult.Data.Path, new()
            {
                DescriptionMd = copyResult.Data.DescriptionMd + " Edited"
            });
            Assert.IsNotNull(updateDescriptionResult);

            await client.Multis.DeleteMulti(copyResult.Data.Path);
        }

        [TestMethod]
        public async Task DeleteSubreddit()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit toCopy = mine.First();
            Assert.IsTrue(toCopy.Data.Subreddits.Any());

            MultiReddit copyResult = await client.Multis.CopyMulti(new()
            {
                From = mine.First().Data.Path,
                To = $"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}"
            });
            Assert.IsNotNull(copyResult);

            await client.Multis.DeleteSubreddit(copyResult.Data.Path, copyResult.Data.Subreddits.First().Name);

            await client.Multis.DeleteMulti(copyResult.Data.Path);
        }

        [TestMethod]
        public async Task GetSubreddit()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit toInspect = mine.First();
            Assert.IsTrue(toInspect.Data.Subreddits.Any());

            MultiSubreddit subredditResult = await client.Multis.GetSubreddit(toInspect.Data.Path, toInspect.Data.Subreddits.First().Name);
            Assert.IsNotNull(subredditResult);
        }

        [TestMethod]
        public async Task AddSubreddit()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(identity.Name));

            List<MultiReddit> mine = await client.Multis.GetMine();
            Assert.IsTrue(mine?.Any());

            MultiReddit toCopy = mine.First();
            Assert.IsTrue(toCopy.Data.Subreddits.Any());

            Listing<Subreddit> subreddits = await client.Subreddits.Get(SubredditsType.Popular);
            Assert.IsNotNull(subreddits);
            Assert.IsTrue(subreddits.Data.Children.Any());

            MultiReddit copyResult = await client.Multis.CopyMulti(new()
            {
                From = mine.First().Data.Path,
                To = $"user/{identity.Name}/m/{DateTime.UtcNow:yyyyMd}"
            });
            Assert.IsNotNull(copyResult);

            MultiSubreddit addResult = await client.Multis.AddSubreddit(copyResult.Data.Path, new()
            {
                SubredditName = subreddits.Data.Children.First().Data.DisplayName
            });
            Assert.IsNotNull(addResult);

            await client.Multis.DeleteMulti(copyResult.Data.Path);
        }
    }
}
