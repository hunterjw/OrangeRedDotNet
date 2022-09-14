using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// Subscribe to or unsubscribe from a subreddit
    /// </summary>
    [Verb("subreddits-subscribe", HelpText = "Subscribe to or unsubscribe from a subreddit")]
    internal class SubscribeVerb : VerbBase
    {
        /// <summary>
        /// Subreddit name
        /// </summary>
        [Option(Required = true, HelpText = "Subreddit name")]
        public string SubredditName { get; set; }
        /// <summary>
        /// Subscribe action
        /// </summary>
        [Option(Required = true, HelpText = "Subscribe action (subscribe or unsubscribe)")]
        public string Action { get; set; }
        /// <summary>
        ///     Set to True to prevent automatically subscribing the user to the current set of 
        ///     defaults when they take their first subscription action. Attempting to set it 
        ///     for an unsubscribe action will result in an error.
        /// </summary>
        [Option(Default = false, HelpText = "Set to True to prevent automatically subscribing the " +
            "user to the current set of defaults when they take their first subscription action." +
            "Attempting to set it for an unsubscribe action will result in an error.")]
        public bool SkipInitialDefaults { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.Subreddits.Subscribe(new()
            {
                SubredditName = SubredditName,
                Action = Action.ToEnumFromDescriptionString<SubscribeAction>(),
                SkipInitialDefaults = SkipInitialDefaults
            });
            return string.Empty;
        }
    }
}
