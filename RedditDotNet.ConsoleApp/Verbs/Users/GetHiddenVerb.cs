using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get hidden Links/Comments for a user
    /// </summary>
    [Verb("users-get-hidden", HelpText = "Get hidden Links/Comments for a user")]
    internal class GetHiddenVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetHidden(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
