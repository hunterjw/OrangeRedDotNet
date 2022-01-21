using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get an overview listing of user submissions
    /// </summary>
    [Verb("users-get-overview", HelpText = "Get an overview listing of user submissions")]
    internal class GetOverviewVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetOverview(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
