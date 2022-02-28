using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

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
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetTrophies(Username)).ToJson();
        }
    }
}
