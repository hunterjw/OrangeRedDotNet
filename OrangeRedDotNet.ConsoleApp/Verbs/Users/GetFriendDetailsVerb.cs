using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get information about a specific 'friend', such as notes.
    /// </summary>
    [Verb("users-get-friend-details", HelpText = "Get information about a specific 'friend', such as notes.")]
    internal class GetFriendDetailsVerb : VerbBase
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        [Option(Required = true, HelpText = "A valid, existing reddit username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetFriendDetails(Username)).ToJson();
        }
    }
}
