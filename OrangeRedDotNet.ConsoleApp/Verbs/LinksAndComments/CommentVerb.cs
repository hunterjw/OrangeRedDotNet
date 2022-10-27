using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.LinkAndComments;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Submit a new comment or reply to a message.
    /// </summary>
    [Verb("linkandcomments-comment", HelpText = "Submit a new comment or reply to a message.")]
    internal class CommentVerb : VerbBase
    {
        /// <summary>
        /// Fullname of parent thing
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of parent thing")]
        public string ThingId { get; set; }

        /// <summary>
        /// Raw markdown text
        /// </summary>
        [Option(Required = true, HelpText = "Raw markdown text")]
        public string Text { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.LinksAndComments.Comment(BuildCommentParameters())).ToJson();
        }

        /// <summary>
        /// Build comment parameters object
        /// </summary>
        /// <returns>Comment parameters object</returns>
        private CommentParameters BuildCommentParameters()
        {
            return new()
            {
                ThingId = ThingId,
                Text = Text,
            };
        }
    }
}
