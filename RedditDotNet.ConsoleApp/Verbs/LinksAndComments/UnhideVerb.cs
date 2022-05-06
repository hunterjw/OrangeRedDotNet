using CommandLine;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Unhide a link.
    /// </summary>
    [Verb("linksandcomments-unhide", HelpText = "Unhide a link.")]
    internal class UnhideVerb : VerbBase
    {
        /// <summary>
        /// Fullname of a link
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of a link")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Unhide(Id);
            return string.Empty;
        }
    }
}
