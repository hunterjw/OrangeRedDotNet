using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Search;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class SearchTests : TestBase
    {
        [TestMethod]
        public async Task Search()
        {
            SearchResults result = await GetRedditClient().Search.Search(new SearchListingParameters
            {
                Query = "test"
            });
            Assert.IsNotNull(result);
        }
    }
}
