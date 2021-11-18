using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
    /// <summary>
    /// Get comments for a Link
    /// </summary>
    [Verb("listings-get-comments", HelpText = "Get comments for a Link")]
    class GetCommentsVerb : VerbBase
    {
        /// <summary>
        /// Link ID
        /// </summary>
        [Option(Required = true, HelpText = "Link ID")]
        public string ArticleId { get; set; }
        /// <summary>
        /// Subreddit for the link
        /// </summary>
        [Option(HelpText = "Subreddit for the link")]
        public string Subreddit { get; set; }
		/// <summary>
		/// (optional) ID36 of a comment in the comment tree to focus on
		/// </summary>
		[Option(HelpText = "ID36 of a comment in the comment tree to focus on")]
		public string CommentId { get; set; }
		/// <summary>
		/// Number of parent comments to be shown, an integer between 0 and 8
		/// </summary>
		[Option(HelpText = "Number of parent comments to be shown, an integer between 0 and 8", Default = 0)]
		public int? Context { get; set; }
		/// <summary>
		/// Maximum depth of subtrees in the thread to get
		/// </summary>
		[Option(HelpText = "Maximum depth of subtrees in the thread to get")]
		public int? Depth { get; set; }
		/// <summary>
		/// Maximum number of comments to return
		/// </summary>
		[Option(HelpText = "Maximum number of comments to return")]
		public int? Limit { get; set; }
		/// <summary>
		/// Show edits to the comments
		/// </summary>
		[Option(HelpText = "Show edits to the comments")]
		public bool? ShowEdits { get; set; }
		/// <summary>
		/// Show media on the comments
		/// </summary>
		[Option(HelpText = "Show media on the comments")]
		public bool? ShowMedia { get; set; }
		/// <summary>
		/// Show "More" in the comment tree
		/// </summary>
		[Option(HelpText = "Show \"More\" in the comment tree")]
		public bool? ShowMore { get; set; }
		/// <summary>
		/// Show Titles in the comment tree
		/// </summary>
		[Option(HelpText = "Show Titles in the comment tree")]
		public bool? ShowTitle { get; set; }
		/// <summary>
		/// Expand the referenced subreddit objects
		/// </summary>
		[Option(HelpText = "Expand the referenced subreddit objects")]
		public bool? ExpandSubreddits { get; set; }
		/// <summary>
		/// Get the comments in a treaded format
		/// </summary>
		[Option(HelpText = "Get the comments in a treaded format")]
		public bool? Threaded { get; set; }
		/// <summary>
		/// An integer between 0 and 50
		/// </summary>
		[Option(HelpText = "An integer between 0 and 50")]
		public int? Truncate { get; set; }
		/// <summary>
		/// The sort on the comments returned
		/// </summary>
		[Option(HelpText = "The sort on the comments returned")]
		public string Sort { get; set; }
		/// <summary>
		/// Visual theme of the comments returned
		/// </summary>
		[Option(HelpText = "Visual theme of the comments returned")]
		public string Theme { get; set; }

		/// <inheritdoc/>
		public override string Run(Reddit reddit)
        {
            return ToJson(reddit.Listings.GetComments(ArticleId, subreddit: Subreddit, parameters: BuildCommentListingParameters()).Result);
        }

		/// <summary>
		/// Build the comment listing parameters
		/// </summary>
		/// <returns>Comment listing parameters object</returns>
        private CommentListingParameters BuildCommentListingParameters()
        {
			return new CommentListingParameters
			{
				CommentId = CommentId,
				Context = Context,
				Depth = Depth,
				Limit = Limit,
				ShowEdits = ShowEdits,
				ShowMedia = ShowMedia,
				ShowMore = ShowMore,
				ShowTitle = ShowTitle,
				ExpandSubreddits = ExpandSubreddits,
				Threaded = Threaded,
				Truncate = Truncate,
				Sort = Sort?.ToEnumFromDescriptionString<CommentSort>(),
				Theme = Theme?.ToEnumFromDescriptionString<CommentTheme>()
			};
        }
    }
}
