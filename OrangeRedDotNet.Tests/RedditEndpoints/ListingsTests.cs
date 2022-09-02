using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Multis;
using OrangeRedDotNet.Models.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class ListingsTests : TestBase
    {
        [TestMethod]
        public async Task GetLinks()
        {
            Listing<Link> result = await GetRedditClient().Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetLinksForSubreddit()
        {
            Reddit client = GetRedditClient();
            Listing<Link> frontPage = await client.Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsTrue(frontPage?.Data?.Children.Any());

            string subreddit = frontPage.Data.Children.First().Data.Subreddit;
            Assert.IsNotNull(subreddit);

            Listing<Link> subredditLinks = await client.Listings.GetLinksForSubreddit(LinkListingType.Hot, subreddit);
            Assert.IsNotNull(subredditLinks);
        }

        [TestMethod]
        public async Task GetLinksForMultireddit()
        {
            Reddit client = GetRedditClient();
            List<MultiReddit> multis = await client.Multis.GetMine();
            Assert.IsTrue(multis?.Any());

            Listing<Link> multiLinks = await client.Listings.GetLinksForMultireddit(LinkListingType.Hot, multis.First().Data.Path);
            Assert.IsNotNull(multiLinks);
        }

        [TestMethod]
        public async Task GetByIds()
        {
            Reddit client = GetRedditClient();
            Listing<Link> frontPage = await client.Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsTrue(frontPage?.Data?.Children.Any());

            string fullName = frontPage.Data.Children.First().Data.Name;
            Assert.IsNotNull(fullName);

            Listing<Link> linksById = await client.Listings.GetByIds(new[] { fullName });
            Assert.IsNotNull(linksById);
        }

        [TestMethod]
        public async Task GetComments()
        {
            Reddit client = GetRedditClient();
            Listing<Link> frontPage = await client.Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsTrue(frontPage?.Data?.Children.Any());

            string id = frontPage.Data.Children.First().Data.Id;
            Assert.IsNotNull(id);

            LinkWithComments linkWithComments = await client.Listings.GetComments(id);
            Assert.IsNotNull(linkWithComments);
        }

        [TestMethod]
        public async Task GetDuplicates()
        {
            Reddit client = GetRedditClient();
            Listing<Link> frontPage = await client.Listings.GetLinks(FrontPageListingType.Best);
            Assert.IsTrue(frontPage?.Data?.Children.Any());

            string id = frontPage.Data.Children.First().Data.Id;
            Assert.IsNotNull(id);

            DuplicateLinks duplicates = await client.Listings.GetDuplicates(id);
            Assert.IsNotNull(duplicates);
        }
    }
}
