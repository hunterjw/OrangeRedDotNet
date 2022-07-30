using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Retrieve additional comments omitted from a base comment tree.
    /// </summary>
    [Verb("linksandcomments-get-morechildren", HelpText = "Retrieve additional comments omitted from a base comment tree.")]
    internal class GetMoreChildrenVerb : VerbBase
    {
        /// <summary>
        /// Fullname of the link whose comments are being fetched
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of the link whose comments are being fetched")]
        public string LinkFullName { get; set; }
        /// <summary>
        /// List of comment ID36s that need to be fetched
        /// </summary>
        [Option(Min = 1, Separator = ',', HelpText = "List of comment ID36s that need to be fetched")]
        public IEnumerable<string> Children { get; set; }
        /// <summary>
        /// The sort on the comments returned
        /// </summary>
        [Option(HelpText = "The sort on the comments returned")]
        public string Sort { get; set; }
        /// <summary>
        /// Maximum depth of subtrees in the thread to get
        /// </summary>
        [Option(HelpText = "Maximum depth of subtrees in the thread to get")]
        public int? Depth { get; set; }
        /// <summary>
        /// Only return the children requested
        /// </summary>
        [Option(HelpText = "Only return the children requested")]
        public bool? LimitChildren { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.LinksAndComments.GetMoreChildren(
                    LinkFullName,
                    Children,
                    !string.IsNullOrWhiteSpace(Sort) ? Sort.ToEnumFromDescriptionString<CommentSort>() : null,
                    Depth,
                    LimitChildren))
                .ToJson();
        }
    }
}
