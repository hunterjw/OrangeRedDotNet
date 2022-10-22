using CommandLine;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Subreddits
{
    /// <summary>
    /// List subreddits that begin with a query string.
    /// </summary>
    [Verb("subreddits-searchsubreddits", HelpText = "List subreddits that begin with a query string.")]
    internal class SearchSubredditsVerb : VerbBase
    {
        /// <summary>
        /// Query string
        /// </summary>
        [Option(Required = true, HelpText = "Query string")]
        public string Query { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Subreddits.SearchSubreddits(BuildSearchSubredditsParameters())).ToJson();
        }

        /// <summary>
        /// Build search subreddits parameters object
        /// </summary>
        /// <returns>Search subreddits parameters object</returns>
        private SearchSubredditsParameters BuildSearchSubredditsParameters()
        {
            return new()
            {
                Query = Query
            };
        }
    }
}
