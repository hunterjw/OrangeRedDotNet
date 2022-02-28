using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get submitted comments for a user
    /// </summary>
    [Verb("users-get-comments", HelpText = "Get submitted comments for a user")]
    internal class GetCommentsVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetComments(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
