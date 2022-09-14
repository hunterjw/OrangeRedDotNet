using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Save a link or comment.
    /// </summary>
    [Verb("linksandcomments-save", HelpText = "Save a link or comment.")]
    internal class SaveVerb : VerbBase
    {
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of a thing")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Save(new() { Id = Id });
            return string.Empty;
        }
    }
}
