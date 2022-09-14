using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Update a MultiReddit description
    /// </summary>
    [Verb("multi-update-description", HelpText = "Update a MultiReddit description")]
    internal class UpdateDescriptionVerb : VerbBase
    {
        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <summary>
        /// Description text (markdown)
        /// </summary>
        [Option(Required = true, HelpText = "Description text (markdown)")]
        public string Description { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.UpdateDescription(Path, new()
            {
                DescriptionMd = Description
            })).ToJson();
        }
    }
}
