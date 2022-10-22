using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Submit a link to a subreddit.
    /// </summary>
    [Verb("linkandcomments-submit", HelpText = "Submit a link to a subreddit.")]
    internal class SubmitVerb : VerbBase
    {
        /// <summary>
        /// Submit kind
        /// </summary>
        [Option(Required = true, HelpText = "Submit kind")]
        public string Kind { get; set; }

        /// <summary>
        /// If the post is NSFW or not
        /// </summary>
        [Option(HelpText = "If the post is NSFW or not")]
        public bool? Nsfw { get; set; }

        /// <summary>
        /// To send reply notifications or not
        /// </summary>
        [Option(HelpText = "To send reply notifications or not")]
        public bool? SendReplies { get; set; }

        /// <summary>
        /// If the post is a spoiler or not
        /// </summary>
        [Option(HelpText = "If the post is a spoiler or not")]
        public bool? Spoiler { get; set; }

        /// <summary>
        /// Subreddit display name
        /// </summary>
        [Option(Required = true, HelpText = "Subreddit display name")]
        public string Subreddit { get; set; }

        /// <summary>
        /// Post text
        /// </summary>
        [Option(HelpText = "Post text")]
        public string Text { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        [Option(HelpText = "Post title")]
        public string Title { get; set; }

        /// <summary>
        /// Link URL
        /// </summary>
        [Option(HelpText = "Link URL")]
        public string Url { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            var parameters = BuildSubmitParameters();
            return (await reddit.LinksAndComments.Submit(
                    parameters.FilterParametersByKind(parameters.Kind))
                ).ToJson();
        }

        /// <summary>
        /// Build a submit parameters object
        /// </summary>
        /// <returns>Submit parameters object</returns>
        private SubmitParameters BuildSubmitParameters()
        {
            return new()
            {
                Kind = Kind.ToEnumFromDescriptionString<SubmitKind>(),
                Nsfw = Nsfw,
                SendReplies = SendReplies,
                Spoiler = Spoiler,
                Subreddit = Subreddit,
                Text = Text,
                Title = Title,
                Url = Url,
            };
        }
    }
}
