using Newtonsoft.Json;

namespace RedditDotNet.Models.Users
{
    public class UserData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_utc")]
        public double CreatedUtc { get; set; }

        [JsonProperty("link_karma")]
        public int LinkKarma { get; set; }

        [JsonProperty("comment_karma")]
        public int CommentKarma { get; set; }

        [JsonProperty("profile_img")]
        public string ProfileImage { get; set; }

        [JsonProperty("profile_color")]
        public string ProfileColor { get; set; }

        [JsonProperty("profile_over_18")]
        public bool ProfileOver18 { get; set; }
    }
}
