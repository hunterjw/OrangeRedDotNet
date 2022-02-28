using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get saved Links/Comments for a user
    /// </summary>
    [Verb("users-get-saved", HelpText = "Get saved Links/Comments for a user")]
    internal class GetSavedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetSaved(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
