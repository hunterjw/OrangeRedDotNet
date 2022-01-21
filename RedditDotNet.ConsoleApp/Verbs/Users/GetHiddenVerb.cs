using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get hidden Links/Comments for a user
    /// </summary>
    [Verb("users-get-hidden", HelpText = "Get hidden Links/Comments for a user")]
    internal class GetHiddenVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetHidden(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
