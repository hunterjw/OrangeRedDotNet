using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Parameters;

namespace RedditDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Abstract class for Users Listing verbs
    /// </summary>
    internal abstract class UsersListingVerb : VerbBase
    {
        /// <summary>
        /// Reddit username
        /// </summary>
        [Option(Required = true, HelpText = "Reddit username")]
        public string Username { get; set; }
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Option(HelpText = "Fullname of a thing")]
        public string After { get; set; }
        /// <summary>
        /// Fullname of a thing
        /// </summary>
        [Option(HelpText = "Fullname of a thing")]
        public string Before { get; set; }
        /// <summary>
        /// Number of items already retrieved
        /// </summary>
        [Option(HelpText = "Number of items already retrieved")]
        public int Count { get; set; } = 0;
        /// <summary>
        /// Maximum number of things to return
        /// </summary>
        [Option(HelpText = "Maximum number of things to return")]
        public int Limit { get; set; } = 25;
        /// <summary>
        /// To show all or not (bypasses preferences that would hide results)
        /// </summary>
        [Option(HelpText = "To show all or not (bypasses preferences that would hide results)")]
        public bool? ShowAll { get; set; }
        /// <summary>
        /// Expand subreddit references into objects
        /// </summary>
        [Option(HelpText = "Expand subreddit references into objects")]
        public bool? ExpandSubreddits { get; set; }
        /// <summary>
        /// Context to include for comments
        /// </summary>
        [Option(HelpText = "Context to include for comments")]
        public int? Context { get; set; }
        /// <summary>
        /// Sort of the results (hot, new, top, controversial)
        /// </summary>
        [Option(HelpText = "Sort of the results (hot, new, top, controversial)")]
        public string Sort { get; set; }
        /// <summary>
        /// Timescale of results (when sort is top or controversial) (hour, day, week, month, year, all)
        /// </summary>
        [Option(HelpText = "Timescale of results (when sort is top or controversial) (hour, day, week, month, year, all)")]
        public string Timescale { get; set; }
        /// <summary>
        /// Limit for what type of results to return (links, comments)
        /// </summary>
        [Option(HelpText = "Limit for what type of results to return (links, comments)")]
        public string Type { get; set; }

        /// <summary>
        /// Build a parameter object from this object
        /// </summary>
        /// <returns>Parameter object</returns>
        protected UsersListingParameters BuildUsersListingParameters()
        {
            return new UsersListingParameters
            {
                After = After,
                Before = Before,
                Count = Count,
                Limit = Limit,
                ShowAll = ShowAll,
                ExpandSubreddits = ExpandSubreddits,
                Context = Context,
                Sort = string.IsNullOrWhiteSpace(Sort) ? null : 
                    Sort.ToEnumFromDescriptionString<UsersListingSort>(),
                Timescale = string.IsNullOrWhiteSpace(Timescale) ? null :
                    Timescale.ToEnumFromDescriptionString<Timescale>(),
                Type = string.IsNullOrWhiteSpace(Type) ? null :
                    Type.ToEnumFromDescriptionString<UsersListingType>(),
            };
        }
    }
}
