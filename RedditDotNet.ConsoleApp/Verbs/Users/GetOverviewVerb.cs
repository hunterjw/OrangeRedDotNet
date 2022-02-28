using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get an overview listing of user submissions
    /// </summary>
    [Verb("users-get-overview", HelpText = "Get an overview listing of user submissions")]
    internal class GetOverviewVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetOverview(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
