using RedditDotNet.Extensions;
using RedditDotNet.Interfaces;
using System.Collections.Generic;

namespace RedditDotNet.Models.Parameters
{
    /// <summary>
    /// Parameters for getting Comments from Reddit
    /// </summary>
    public class CommentListingParameters : IQueryParameters
    {
        /// <summary>
        /// (optional) ID36 of a comment in the comment tree to focus on
        /// </summary>
        public string CommentId { get; set; }
        /// <summary>
        /// Number of parent comments to be shown, an integer between 0 and 8
        /// </summary>
        public int? Context { get; set; }
        /// <summary>
        /// Maximum depth of subtrees in the thread to get
        /// </summary>
        public int? Depth { get; set; }
        /// <summary>
        /// Maximum number of comments to return
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Show edits to the comments
        /// </summary>
        public bool? ShowEdits { get; set; }
        /// <summary>
        /// Show media on the comments
        /// </summary>
        public bool? ShowMedia { get; set; }
        /// <summary>
        /// Show "More" in the comment tree
        /// </summary>
        public bool? ShowMore { get; set; }
        /// <summary>
        /// Show Titles in the comment tree
        /// </summary>
        public bool? ShowTitle { get; set; }
        /// <summary>
        /// Expand the referenced subreddit objects
        /// </summary>
        public bool? ExpandSubreddits { get; set; }
        /// <summary>
        /// Get the comments in a treaded format
        /// </summary>
        public bool? Threaded { get; set; }
        /// <summary>
        /// An integer between 0 and 50
        /// </summary>
        public int? Truncate { get; set; }
        /// <summary>
        /// The sort on the comments returned
        /// </summary>
        public CommentSort? Sort { get; set; }
        /// <summary>
        /// Visual theme of the comments returned
        /// </summary>
        public CommentTheme? Theme { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> ToQueryParameters()
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(CommentId))
            {
                dict.Add("comment", CommentId);
            }
            if (Context.HasValue)
            {
                dict.Add("context", $"{Context.Value}");
            }
            if (Depth.HasValue)
            {
                dict.Add("depth", $"{Depth.Value}");
            }
            if (Limit.HasValue)
            {
                dict.Add("limit", $"{Limit.Value}");
            }
            if (ShowEdits.HasValue)
            {
                dict.Add("showedits", $"{ShowEdits.Value}");
            }
            if (ShowMedia.HasValue)
            {
                dict.Add("showmedia", $"{ShowMedia.Value}");
            }
            if (ShowMore.HasValue)
            {
                dict.Add("showmore", $"{ShowMore.Value}");
            }
            if (ShowTitle.HasValue)
            {
                dict.Add("showtitle", $"{ShowTitle.Value}");
            }
            if (ExpandSubreddits.HasValue)
            {
                dict.Add("sr_detail", $"{ExpandSubreddits.Value}");
            }
            if (Threaded.HasValue)
            {
                dict.Add("threaded", $"{Threaded.Value}");
            }
            if (Truncate.HasValue)
            {
                dict.Add("truncate", $"{Truncate.Value}");
            }
            if (Sort.HasValue)
            {
                dict.Add("sort", Sort.Value.ToDescriptionString());
            }
            if (Theme.HasValue)
            {
                dict.Add("theme", Theme.Value.ToDescriptionString());
            }
            return dict;
        }

        /// <inheritdoc/>
        public IList<string> GetValidationErrors()
        {
            List<string> errors = new();

            if (Context.HasValue && (Context < 0 || Context > 8))
            {
                errors.Add($"{nameof(Context)} must be between 0 and 8");
            }
            if (Depth.HasValue && Depth < 0)
            {
                errors.Add($"{nameof(Depth)} must be a positive integer");
            }
            if (Limit.HasValue && Limit < 0)
            {
                errors.Add($"{nameof(Limit)} must be a positive integer");
            }
            if (Truncate.HasValue && (Truncate < 0 || Truncate > 50))
            {
                errors.Add($"{nameof(Truncate)} must be between 0 and 50");
            }

            return errors;
        }
    }
}
