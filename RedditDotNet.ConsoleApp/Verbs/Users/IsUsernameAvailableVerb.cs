using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
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
        public override string Run(Reddit reddit)
        {
            return reddit.Users.IsUsernameAvailable(Username).Result.ToJson();
        }
    }
}
