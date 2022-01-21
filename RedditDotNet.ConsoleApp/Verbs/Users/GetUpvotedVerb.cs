using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get upvoted Links/Comments for a user
    /// </summary>
    [Verb("users-get-upvoted", HelpText = "Get upvoted Links/Comments for a user")]
    internal class GetUpvotedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetUpvoted(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
