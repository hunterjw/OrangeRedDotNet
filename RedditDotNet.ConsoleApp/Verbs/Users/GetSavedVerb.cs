using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get saved Links/Comments for a user
    /// </summary>
    [Verb("users-get-saved", HelpText = "Get saved Links/Comments for a user")]
    internal class GetSavedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetSaved(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
