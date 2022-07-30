using CommandLine;
using OrangeRedDotNet.ConsoleApp.Verbs.Listings;
using OrangeRedDotNet.Extensions;
using OrangeRedDotNet.Models.Parameters;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs.Users
{
    /// <summary>
    /// Abstract class for Users Listing verbs
    /// </summary>
    [Verb("users-get-listing")]
    internal class GetListingVerb : ListingVerb
    {
        /// <summary>
        /// Reddit username
        /// </summary>
        [Option(Required = true, HelpText = "Reddit username")]
        public string Username { get; set; }
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
        /// Listing type (overview, submitted, comments, upvoted, downvoted, hidden, saved, gilded)
        /// </summary>
        [Option(Required = true, HelpText = "Listing type (overview, submitted, comments, upvoted, downvoted, hidden, saved, gilded)")]
        public string ListingType { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            return (await reddit.Users.GetListing(
                    Username, 
                    ListingType.ToEnumFromDescriptionString<UserProfileListingType>(), 
                    BuildUsersListingParameters()))
                .ToJson();
        }

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
