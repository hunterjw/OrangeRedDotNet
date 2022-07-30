using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to delete a MultiReddit
    /// </summary>
    [Verb("multi-delete", HelpText = "Delete a MultiReddit")]
    internal class DeleteVerb : VerbBase
    {
        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.Multis.DeleteMulti(Path);
            return string.Empty;
        }
    }
}
