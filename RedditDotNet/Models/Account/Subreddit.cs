using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Account
{
	public class Subreddit
    {
        [JsonProperty("default_set")]
        public bool DefaultSet { get; set; }

        [JsonProperty("user_is_contributor")]
        public bool UserIsContributor { get; set; }

        [JsonProperty("banner_img")]
        public string BannerImg { get; set; }

        [JsonProperty("restrict_posting")]
        public bool RestrictPosting { get; set; }

        [JsonProperty("user_is_banned")]
        public bool UserIsBanned { get; set; }

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports { get; set; }

        [JsonProperty("community_icon")]
        public object CommunityIcon { get; set; }

        [JsonProperty("show_media")]
        public bool ShowMedia { get; set; }

        [JsonProperty("icon_color")]
        public string IconColor { get; set; }

        [JsonProperty("user_is_muted")]
        public bool UserIsMuted { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("header_img")]
        public object HeaderImg { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("previous_names")]
        public List<object> PreviousNames { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("icon_size")]
        public List<int> IconSize { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel { get; set; }

        [JsonProperty("header_size")]
        public object HeaderSize { get; set; }

        [JsonProperty("restrict_commenting")]
        public bool RestrictCommenting { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel { get; set; }

        [JsonProperty("is_default_icon")]
        public bool IsDefaultIcon { get; set; }

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition { get; set; }

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_default_banner")]
        public bool IsDefaultBanner { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("quarantine")]
        public bool Quarantine { get; set; }

        [JsonProperty("banner_size")]
        public object BannerSize { get; set; }

        [JsonProperty("user_is_moderator")]
        public bool UserIsModerator { get; set; }

        [JsonProperty("accept_followers")]
        public bool AcceptFollowers { get; set; }

        [JsonProperty("public_description")]
        public string PublicDescription { get; set; }

        [JsonProperty("link_flair_enabled")]
        public bool LinkFlairEnabled { get; set; }

        [JsonProperty("disable_contributor_requests")]
        public bool DisableContributorRequests { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("user_is_subscriber")]
        public bool UserIsSubscriber { get; set; }
    }
}
