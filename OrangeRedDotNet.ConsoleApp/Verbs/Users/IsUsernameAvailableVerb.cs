using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Check whether a username is available for registration.
    /// </summary>
    [Verb("users-is-username-available", HelpText = "Check whether a username is available for registration.")]
    internal class IsUsernameAvailableVerb : VerbBase
    {
        /// <summary>
        /// A valid, unused, username
        /// </summary>
        [Option(Required = true, HelpText = "A valid, unused, username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.IsUsernameAvailable(new()
            {
                Username = Username
            })).ToJson();
        }
    }
}
