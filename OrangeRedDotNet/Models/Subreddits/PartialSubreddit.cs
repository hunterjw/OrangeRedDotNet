using Newtonsoft.Json;

namespace OrangeRedDotNet.Models.Subreddits
{
    public class PartialSubreddit
    {
        [JsonProperty("active_user_count")]
        public int ActiveUserCount { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subscriber_count")]
        public int SubscriberCount { get; set; }

        [JsonProperty("is_chat_post_feature_enabled")]
        public bool IsChatPostFeatureEnabled { get; set; }

        [JsonProperty("allow_chat_post_creation")]
        public bool AllowChatPostCreation { get; set; }

        [JsonProperty("allow_images")]
        public bool AllowImages { get; set; }
    }
}
