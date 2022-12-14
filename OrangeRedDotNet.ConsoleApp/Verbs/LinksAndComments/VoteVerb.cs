using CommandLine;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Cast a vote on a thing.
    /// </summary>
    [Verb("linksandcomments-vote", HelpText = "Cast a vote on a thing.")]
    internal class VoteVerb : VerbBase
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        [Option(Required = true, HelpText = "fullname of a thing")]
        public string Id { get; set; }

        /// <summary>
        /// vote direction. one of (1, 0, -1)
        /// </summary>
        [Option(Required = true, HelpText = "vote direction. one of (1, 0, -1)")]
        public int Dir { get; set; }

        /// <summary>
        /// an integer greater than 1
        /// </summary>
        [Option(Required = true, HelpText = "an integer greater than 1")]
        public int Rank { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Vote(new()
            {
                Id = Id,
                Dir = Dir,
                Rank = Rank
            });
            return string.Empty;
        }
    }
}
