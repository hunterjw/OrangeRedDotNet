using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Account
{
	public class SubredditKarmaBreakdown
	{
		[JsonProperty("sr")]
		public string Subreddit { get; set; }

		[JsonProperty("comment_karma")]
		public int CommentKarma { get; set; }

		[JsonProperty("link_karma")]
		public int LinkKarma { get; set; }
	}
}
