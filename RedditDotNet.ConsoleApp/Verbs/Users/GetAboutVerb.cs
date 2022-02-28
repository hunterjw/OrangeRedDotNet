using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Return information about the user, including karma and gold status.
    /// </summary>
    [Verb("users-get-about", HelpText = "Return information about the user, including karma and gold status.")]
    internal class GetAboutVerb : VerbBase
    {
        /// <summary>
        /// The name of an existing user
        /// </summary>
        [Option(Required = true, HelpText = "The name of an existing user")]
        public string Username { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetAbout(Username)).ToJson();
        }
    }
}
