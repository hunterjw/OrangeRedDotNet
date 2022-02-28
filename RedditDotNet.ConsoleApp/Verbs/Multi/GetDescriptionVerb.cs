using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Get a description for a MultiReddit
    /// </summary>
    [Verb("multi-get-description", HelpText = "Get a description for a MultiReddit")]
    internal class GetDescriptionVerb : VerbBase
    {
        /// <summary>
        /// MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "MultiReddit path")]
        public string Path { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.GetDescription(Path)).ToJson();
        }
    }
}
