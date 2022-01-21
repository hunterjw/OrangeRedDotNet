using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get gilded Links/Comments for a user
    /// </summary>
    [Verb("users-get-gilded", HelpText = "Get gilded Links/Comments for a user")]
    internal class GetGildedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetGilded(Username, BuildUsersListingParameters()).Result.ToJson();
        }
    }
}
