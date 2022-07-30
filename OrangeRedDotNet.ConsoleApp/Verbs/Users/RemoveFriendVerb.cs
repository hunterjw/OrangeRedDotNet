using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Stop being friends with a user.
    /// </summary>
    [Verb("users-remove-friend", HelpText = "Stop being friends with a user.")]
    internal class RemoveFriendVerb : VerbBase
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        [Option(Required = true, HelpText = "A valid, existing reddit username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.Users.RemoveFriend(Username);
            return string.Empty;
        }
    }
}
