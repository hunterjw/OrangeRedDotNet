using OrangeRedDotNet.Extensions;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// Submit parameters
    /// </summary>
    public class SubmitParameters : QueryParametersBase
    {
        /// <summary>
        /// Submit kind
        /// </summary>
        public SubmitKind Kind { get; set; }
        /// <summary>
        /// Subreddit display name
        /// </summary>
        public string Subreddit { get; set; }
        /// <summary>
        /// Post title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Post text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Link URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// If the post is NSFW or not
        /// </summary>
        public bool? Nsfw { get; set; }
        /// <summary>
        /// If the post is a spoiler or not
        /// </summary>
        public bool? Spoiler { get; set; }
        /// <summary>
        /// To send reply notifications or not
        /// </summary>
        public bool? SendReplies { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            Dictionary<string, string> toReturn = new()
            {
                { "api_type", "json" },
                { "kind", Kind.ToDescriptionString() },
                { "sr", Subreddit },
                { "title", Title },
            };
            if (!string.IsNullOrWhiteSpace(Text))
            {
                toReturn.Add("text", Text);
            }
            if (!string.IsNullOrWhiteSpace(Url))
            {
                toReturn.Add("url", Url);
            }
            if (Nsfw.HasValue)
            {
                toReturn.Add("nsfw", Nsfw.ToString());
            }
            if (Spoiler.HasValue)
            {
                toReturn.Add("spoiler", Spoiler.ToString());
            }
            if (SendReplies.HasValue)
            {
                toReturn.Add("sendreplies", SendReplies.ToString());
            }
            return toReturn;
        }

        /// <summary>
        /// Return a new SubmitParameters object with the contents filtered based on a <paramref name="kind"/>.
        /// </summary>
        /// <param name="kind">Submit kind</param>
        /// <returns>New SubmitParameters object</returns>
        public SubmitParameters FilterParametersByKind(SubmitKind kind)
        {
            return kind switch
            {
                SubmitKind.Self => new SubmitParameters
                {
                    Kind = kind,
                    Subreddit = Subreddit,
                    Title = Title,
                    Text = Text,
                    Nsfw = Nsfw,
                    Spoiler = Spoiler,
                    SendReplies = SendReplies,
                },
                SubmitKind.Link => new SubmitParameters
                {
                    Kind = kind,
                    Subreddit = Subreddit,
                    Title = Title,
                    Url = Url,
                    Nsfw = Nsfw,
                    Spoiler = Spoiler,
                    SendReplies = SendReplies,
                },
                _ => default
            };
        }
    }
}
