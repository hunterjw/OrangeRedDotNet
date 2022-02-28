using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get upvoted Links/Comments for a user
    /// </summary>
    [Verb("users-get-upvoted", HelpText = "Get upvoted Links/Comments for a user")]
    internal class GetUpvotedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetUpvoted(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
