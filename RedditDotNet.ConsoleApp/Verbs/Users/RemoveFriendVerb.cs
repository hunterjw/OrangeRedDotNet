using CommandLine;

namespace RedditDotNet.ConsoleApp.Verbs.Users
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
        public override string Run(Reddit reddit)
        {
            reddit.Users.RemoveFriend(Username).Wait();
            return string.Empty;
        }
    }
}
