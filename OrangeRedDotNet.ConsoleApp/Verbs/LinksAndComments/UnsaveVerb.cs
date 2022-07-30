using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Unsave a link or comment.
    /// </summary>
    [Verb("linksandcomments-unsave", HelpText = "Unsave a link or comment.")]
    internal class UnsaveVerb : VerbBase
    {
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of a thing")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Unsave(Id);
            return string.Empty;
        }
    }
}
