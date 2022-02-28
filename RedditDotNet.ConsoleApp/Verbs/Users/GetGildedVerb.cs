using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get gilded Links/Comments for a user
    /// </summary>
    [Verb("users-get-gilded", HelpText = "Get gilded Links/Comments for a user")]
    internal class GetGildedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetGilded(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
