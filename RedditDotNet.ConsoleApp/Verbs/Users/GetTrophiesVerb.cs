using CommandLine;
using RedditDotNet.Extensions;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Return a list of trophies for the a given user.
    /// </summary>
    [Verb("users-get-trophies", HelpText = "Return a list of trophies for the a given user.")]
    internal class GetTrophiesVerb : VerbBase
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        [Option(Required = true, HelpText = "A valid, existing reddit username")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Users.GetTrophies(Username).Result.ToJson();
        }
    }
}
