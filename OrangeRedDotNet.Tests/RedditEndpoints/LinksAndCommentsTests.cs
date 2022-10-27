using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrangeRedDotNet.Interfaces;
using OrangeRedDotNet.Models.Account;
using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.LinksAndComments;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Parameters.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Tests.RedditEndpoints
{
    [TestClass]
    public class LinksAndCommentsTests : TestBase
    {
        [TestMethod]
        public async Task Vote_Upvote()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Listing<ILinkOrComment> upvoted = await client.Users.GetListing(identity.Name, UserProfileListingType.Upvoted, new UsersListingParameters
            {
                Sort = UsersListingSort.New
            });
            Assert.IsTrue(upvoted?.Data?.Count > 0, "Couldn't find test posts to upvote");

            ILinkOrComment toUpvote = upvoted.Data.Children.FirstOrDefault();
            Assert.IsInstanceOfType(toUpvote, typeof(Link));

            Link linkToUpvote = toUpvote as Link;
            await client.LinksAndComments.Vote(new()
            {
                Id = linkToUpvote.Data.Name,
                Dir = 1,
                Rank = 2
            });
        }

        [TestMethod]
        public async Task Vote_Downvote()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Listing<ILinkOrComment> downvoted = await client.Users.GetListing(identity.Name, UserProfileListingType.Downvoted, new UsersListingParameters
            {
                Sort = UsersListingSort.New
            });
            Assert.IsTrue(downvoted?.Data?.Count > 0, "Couldn't find test posts to downvote");

            ILinkOrComment toDownvote = downvoted.Data.Children.FirstOrDefault();
            Assert.IsInstanceOfType(toDownvote, typeof(Link));

            Link linkToDownvote = toDownvote as Link;
            await client.LinksAndComments.Vote(new()
            {
                Id = linkToDownvote.Data.Name,
                Dir = -1,
                Rank = 2
            });
        }


        [TestMethod]
        public async Task Vote_ClearVote()
        {
            Reddit client = GetRedditClient();
            Listing<Link> links = await client.Listings.GetLinks(FrontPageListingType.New);
            Assert.IsTrue(links?.Data?.Count > 0, "Couldn't find test posts to clear vote");

            Link linkToClearVote = links.Data.Children.FirstOrDefault(_ => !_.Data.Likes.HasValue);
            Assert.IsNotNull(linkToClearVote);

            await client.LinksAndComments.Vote(new()
            {
                Id = linkToClearVote.Data.Name,
                Dir = 0,
                Rank = 2
            });
        }

        [TestMethod]
        public async Task GetMoreChildren()
        {
            Reddit client = GetRedditClient();
            Listing<Link> links = await client.Listings.GetLinks(FrontPageListingType.Top, new SortListingParameters
            {
                Timescale = Timescale.Day
            });
            Assert.IsTrue(links?.Data?.Count > 0, "Couldn't find test posts to look for more comments");

            string linkFullName = null;
            IEnumerable<string> children = null;
            foreach (Link link in links.Data.Children)
            {
                LinkWithComments linkWithComments = await client.Listings.GetComments(link.Data.Id);
                IEnumerable<CommentBase> moreData = linkWithComments.Comments.Data.Children.Where(_ => _.Data.GetType() == typeof(MoreData));
                if (moreData.Any())
                {
                    linkFullName = link.Data.Name;
                    children = (moreData.First().Data as MoreData).Children.Take(100);
                    break;
                }
            }
            Assert.IsNotNull(linkFullName);
            Assert.IsNotNull(children);

            List<CommentBase> moreChildren = await client.LinksAndComments.GetMoreChildren(
                new()
                {
                    LinkFullName = linkFullName,
                    Children = children
                });
            Assert.IsNotNull(moreChildren);
        }

        [TestMethod]
        public async Task Save()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Listing<ILinkOrComment> saved = await client.Users.GetListing(identity.Name, UserProfileListingType.Saved, new UsersListingParameters
            {
                Sort = UsersListingSort.New
            });
            Assert.IsTrue(saved?.Data?.Count > 0, "Couldn't find test things to save");

            ILinkOrComment first = saved.Data.Children.First();
            string fullName = string.Empty;
            if (typeof(Link).IsInstanceOfType(first))
            {
                fullName = ((Link)first).Data.Name;
            }
            else if (typeof(CommentBase).IsInstanceOfType(first))
            {
                fullName = ((CommentBase)first).Data.Name;
            }
            Assert.IsTrue(!string.IsNullOrWhiteSpace(fullName));

            await client.LinksAndComments.Save(new()
            {
                Id = fullName
            });
        }

        [TestMethod]
        public async Task Unsave()
        {
            Reddit client = GetRedditClient();
            Listing<Link> links = await client.Listings.GetLinks(FrontPageListingType.New);
            Assert.IsTrue(links?.Data?.Count > 0, "Couldn't find test posts to unsave");

            Link linkToUnsave = links.Data.Children.FirstOrDefault(_ => !_.Data.Saved);
            Assert.IsNotNull(linkToUnsave);

            await client.LinksAndComments.Unsave(new()
            {
                Id = linkToUnsave.Data.Name
            });
        }

        [TestMethod]
        public async Task Hide()
        {
            Reddit client = GetRedditClient();
            AccountData identity = await client.Account.GetIdentity();
            Listing<ILinkOrComment> saved = await client.Users.GetListing(identity.Name, UserProfileListingType.Hidden, new UsersListingParameters
            {
                Sort = UsersListingSort.New
            });
            Assert.IsTrue(saved?.Data?.Count > 0, "Couldn't find test things to hide");

            ILinkOrComment first = saved.Data.Children.First();
            string fullName = string.Empty;
            if (typeof(Link).IsInstanceOfType(first))
            {
                fullName = ((Link)first).Data.Name;
            }
            Assert.IsTrue(!string.IsNullOrWhiteSpace(fullName));

            await client.LinksAndComments.Hide(new()
            {
                Id = fullName
            });
        }

        [TestMethod]
        public async Task Unhide()
        {
            Reddit client = GetRedditClient();
            Listing<Link> links = await client.Listings.GetLinks(FrontPageListingType.New);
            Assert.IsTrue(links?.Data?.Count > 0, "Couldn't find test posts to unhide");

            Link linkToUnsave = links.Data.Children.FirstOrDefault(_ => !_.Data.Hidden);
            Assert.IsNotNull(linkToUnsave);

            await client.LinksAndComments.Unhide(new()
            {
                Id = linkToUnsave.Data.Name
            });
        }

        [TestMethod]
        public async Task Submit()
        {
            await RunWithTestSubreddit(async (testSubreddit) =>
            {
                Reddit client = GetRedditClient();

                SubmitResponse result = await client.LinksAndComments.Submit(new()
                {
                    Kind = SubmitKind.Self,
                    Subreddit = testSubreddit,
                    Title = $"Test Post {Guid.NewGuid()}",
                    Text = $"Test post to {testSubreddit}"
                });
                Assert.IsNotNull(result);
                Assert.IsTrue(!(result?.Content?.Errors?.Any()));
            });
        }

        [TestMethod]
        public async Task Comment()
        {
            await RunWithTestPost(async (testPostThingId) =>
            {
                var client = GetRedditClient();
                var result = await client.LinksAndComments.Comment(new()
                {
                    ThingId = testPostThingId,
                    Text = $"Test comment {Guid.NewGuid()}"
                });
                Assert.IsNotNull(result);
                Assert.IsTrue(!(result?.Content?.Errors?.Any()));
            });
        }
    }
}
