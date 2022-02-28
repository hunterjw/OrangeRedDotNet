using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Create or update a 'friend' relationship.
    /// </summary>
    [Verb("users-update-friend", HelpText = "Create or update a 'friend' relationship.")]
    internal class UpdateFriendVerb : VerbBase
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        [Option(Required = true, HelpText = "A valid, existing reddit username")]
        public string Username { get; set; }
        /// <summary>
        /// A string no longer than 300 characters
        /// </summary>
        [Option(Default = "", HelpText = "A string no longer than 300 characters")]
        public string Note { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.UpdateFriend(Username, Note)).ToJson();
        }
    }
}
