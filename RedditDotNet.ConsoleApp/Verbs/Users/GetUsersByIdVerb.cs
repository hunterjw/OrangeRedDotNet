using CommandLine;
using RedditDotNet.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Get user data by IDs
    /// </summary>
    [Verb("users-get-by-ids", HelpText = "Get user data by IDs")]
    internal class GetUsersByIdVerb : VerbBase
    {
        /// <summary>
        /// Account fullnames
        /// </summary>
        [Option(Required = true, HelpText = "Account fullnames")]
        public IEnumerable<string> Users { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetUsersByIds(Users)).ToJson();
        }
    }
}
