using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Hide a link.
    /// </summary>
    [Verb("linksandcomments-hide", HelpText = "Hide a link.")]
    internal class HideVerb : VerbBase
    {
        /// <summary>
        /// Fullname of a link
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of a link")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Hide(new() { Id = Id });
            return string.Empty;
        }
    }
}
