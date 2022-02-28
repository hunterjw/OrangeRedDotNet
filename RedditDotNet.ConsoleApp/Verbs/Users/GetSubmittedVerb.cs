using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get submitted links for a user
    /// </summary>
    [Verb("users-get-submitted", HelpText = "Get submitted links for a user")]
    internal class GetSubmittedVerb : UsersListingVerb
    {
        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetSubmitted(Username, BuildUsersListingParameters())).ToJson();
        }
    }
}
