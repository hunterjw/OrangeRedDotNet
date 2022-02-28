using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get downvoted Links/Comments for a user
    /// </summary>
    [Verb("users-get-downvoted", HelpText = "Get downvoted Links/Comments for a user")]
    internal class GetDownvotedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetDownvoted(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
