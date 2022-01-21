using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get submitted links for a user
    /// </summary>
    [Verb("users-get-submitted", HelpText = "Get submitted links for a user")]
    internal class GetSubmittedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetSubmitted(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
