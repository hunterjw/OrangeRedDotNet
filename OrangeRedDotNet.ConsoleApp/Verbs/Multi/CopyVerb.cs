using CommandLine;
using OrangeRedDotNet.Extensions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to copy a MultiReddit
    /// </summary>
    [Verb("multi-copy", HelpText = "Copy a MultiReddit")]
    internal class CopyVerb : VerbBase
    {
        /// <summary>
        /// From MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "From MultiReddit path")]
        public string From { get; set; }

        /// <summary>
        /// To MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "To MultiReddit path")]
        public string To { get; set; }

        /// <summary>
        /// Display name for the new MultiReddit
        /// </summary>
        [Option(HelpText = "Display name for the new MultiReddit")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Description (markdown) for the new MultiReddit
        /// </summary>
        [Option(HelpText = "Description (markdown) for the new MultiReddit")]
        public string Description { get; set; }

        /// <summary>
        /// Expand the Subreddits in the result
        /// </summary>
        [Option(HelpText = "Expand the Subreddits in the result")]
        public bool? ExpandSubreddits { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Multis.CopyMulti(
                From, To, DisplayName, Description, 
                ExpandSubreddits)).ToJson();
        }
    }
}
