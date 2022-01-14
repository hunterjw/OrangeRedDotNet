using CommandLine;
using RedditDotNet.Extensions;

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
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.GetDescription(Path).Result.ToJson();
        }
    }
}
