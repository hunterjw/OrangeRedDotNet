using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get submitted comments for a user
    /// </summary>
    [Verb("users-get-comments", HelpText = "Get submitted comments for a user")]
    internal class GetCommentsVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetComments(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
