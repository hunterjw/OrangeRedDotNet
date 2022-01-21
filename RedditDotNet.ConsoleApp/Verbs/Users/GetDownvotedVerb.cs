using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get downvoted Links/Comments for a user
    /// </summary>
    [Verb("users-get-downvoted", HelpText = "Get downvoted Links/Comments for a user")]
    internal class GetDownvotedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetDownvoted(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
