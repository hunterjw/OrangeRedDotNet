using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Subreddits
{
    /// <summary>
    /// Subreddit details
    /// </summary>
	public class Subreddit
    {
        [JsonProperty("accept_followers")]
        public bool AcceptFollowers { get; set; }

        [JsonProperty("accounts_active")]
        public int AccountsActive { get; set; }

        [JsonProperty("accounts_active_is_fuzzed")]
        public bool AccountsActiveIsFuzzed { get; set; }

        [JsonProperty("active_user_count")]
        public int ActiveUserCount { get; set; }

        [JsonProperty("advertiser_category")]
        public string AdvertiserCategory { get; set; }

        [JsonProperty("all_original_content")]
        public bool AllOriginalContent { get; set; }

        [JsonProperty("allow_discovery")]
        public bool AllowDiscovery { get; set; }

        [JsonProperty("allow_galleries")]
        public bool AllowGalleries { get; set; }

        [JsonProperty("allow_images")]
        public bool AllowImages { get; set; }

        [JsonProperty("allow_polls")]
        public bool AllowPolls { get; set; }

        [JsonProperty("allow_prediction_contributors")]
        public bool AllowPredictionContributors { get; set; }

        [JsonProperty("allow_predictions")]
        public bool AllowPredictions { get; set; }

        [JsonProperty("allow_predictions_tournament")]
        public bool AllowPredictionsTournament { get; set; }

        [JsonProperty("allow_talks")]
        public bool AllowTalks { get; set; }

        [JsonProperty("allow_videogifs")]
        public bool AllowVideogifs { get; set; }

        [JsonProperty("allow_videos")]
        public bool AllowVideos { get; set; }

        [JsonProperty("banner_background_color")]
        public string BannerBackgroundColor { get; set; }

        [JsonProperty("banner_background_image")]
        public string BannerBackgroundImage { get; set; }

        [JsonProperty("banner_img")]
        public string BannerImg { get; set; }

        [JsonProperty("banner_size")]
        public List<int> BannerSize { get; set; }

        [JsonProperty("can_assign_link_flair")]
        public bool CanAssignLinkFlair { get; set; }

        [JsonProperty("can_assign_user_flair")]
        public bool CanAssignUserFlair { get; set; }

        [JsonProperty("collapse_deleted_comments")]
        public bool CollapseDeletedComments { get; set; }

        [JsonProperty("comment_score_hide_mins")]
        public int CommentScoreHideMins { get; set; }

        [JsonProperty("community_icon")]
        public string CommunityIcon { get; set; }

        [JsonProperty("community_reviewed")]
        public bool CommunityReviewed { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("created_utc")]
        public double CreatedUtc { get; set; }

        [JsonProperty("default_set")]
        public bool? DefaultSet { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("disable_contributor_requests")]
        public bool DisableContributorRequests { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("display_name_prefixed")]
        public string DisplayNamePrefixed { get; set; }

        [JsonProperty("emojis_custom_size")]
        public object EmojisCustomSize { get; set; }

        [JsonProperty("emojis_enabled")]
        public bool EmojisEnabled { get; set; }

        [JsonProperty("free_form_reports")]
        public bool FreeFormReports { get; set; }

        [JsonProperty("has_menu_widget")]
        public bool HasMenuWidget { get; set; }

        [JsonProperty("header_img")]
        public string HeaderImg { get; set; }

        [JsonProperty("header_size")]
        public List<int> HeaderSize { get; set; }

        [JsonProperty("header_title")]
        public string HeaderTitle { get; set; }

        [JsonProperty("hide_ads")]
        public bool HideAds { get; set; }

        [JsonProperty("icon_color")]
        public string IconColor { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("icon_size")]
        public List<int> IconSize { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_crosspostable_subreddit")]
        public object IsCrosspostableSubreddit { get; set; }

        [JsonProperty("is_enrolled_in_new_modmail")]
        public object IsEnrolledInNewModmail { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("link_flair_enabled")]
        public bool LinkFlairEnabled { get; set; }

        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition { get; set; }

        [JsonProperty("mobile_banner_image")]
        public string MobileBannerImage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notification_level")]
        public string NotificationLevel { get; set; }

        [JsonProperty("original_content_tag_enabled")]
        public bool OriginalContentTagEnabled { get; set; }

        [JsonProperty("over18")]
        public bool Over18 { get; set; }

        [JsonProperty("over_18")]
        private bool Over_18 { set { Over18 = value; } }

        [JsonProperty("prediction_leaderboard_entry_type")]
        public string PredictionLeaderboardEntryType { get; set; }

        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("previous_names")]
        public List<object> PreviousNames { get; set; }

        [JsonProperty("public_description")]
        public string PublicDescription { get; set; }

        [JsonProperty("public_description_html")]
        public string PublicDescriptionHtml { get; set; }

        [JsonProperty("public_traffic")]
        public bool PublicTraffic { get; set; }

        [JsonProperty("quarantine")]
        public bool Quarantine { get; set; }

        [JsonProperty("restrict_commenting")]
        public bool RestrictCommenting { get; set; }

        [JsonProperty("restrict_posting")]
        public bool RestrictPosting { get; set; }

        [JsonProperty("should_archive_posts")]
        public bool ShouldArchivePosts { get; set; }

        [JsonProperty("show_media")]
        public bool ShowMedia { get; set; }

        [JsonProperty("show_media_preview")]
        public bool ShowMediaPreview { get; set; }

        [JsonProperty("spoilers_enabled")]
        public bool SpoilersEnabled { get; set; }

        [JsonProperty("submission_type")]
        public string SubmissionType { get; set; }

        [JsonProperty("submit_link_label")]
        public string SubmitLinkLabel { get; set; }

        [JsonProperty("submit_text")]
        public string SubmitText { get; set; }

        [JsonProperty("submit_text_html")]
        public string SubmitTextHtml { get; set; }

        [JsonProperty("submit_text_label")]
        public string SubmitTextLabel { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("suggested_comment_sort")]
        public object SuggestedCommentSort { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("user_can_flair_in_sr")]
        public object UserCanFlairInSr { get; set; }

        [JsonProperty("user_flair_background_color")]
        public object UserFlairBackgroundColor { get; set; }

        [JsonProperty("user_flair_css_class")]
        public object UserFlairCssClass { get; set; }

        [JsonProperty("user_flair_enabled_in_sr")]
        public bool UserFlairEnabledInSr { get; set; }

        [JsonProperty("user_flair_position")]
        public string UserFlairPosition { get; set; }

        [JsonProperty("user_flair_richtext")]
        public List<object> UserFlairRichtext { get; set; }

        [JsonProperty("user_flair_template_id")]
        public object UserFlairTemplateId { get; set; }

        [JsonProperty("user_flair_text")]
        public object UserFlairText { get; set; }

        [JsonProperty("user_flair_text_color")]
        public object UserFlairTextColor { get; set; }

        [JsonProperty("user_flair_type")]
        public string UserFlairType { get; set; }

        [JsonProperty("user_has_favorited")]
        public bool UserHasFavorited { get; set; }

        [JsonProperty("user_is_banned")]
        public bool UserIsBanned { get; set; }

        [JsonProperty("user_is_contributor")]
        public bool UserIsContributor { get; set; }

        [JsonProperty("user_is_moderator")]
        public bool UserIsModerator { get; set; }

        [JsonProperty("user_is_muted")]
        public bool UserIsMuted { get; set; }

        [JsonProperty("user_is_subscriber")]
        public bool UserIsSubscriber { get; set; }

        [JsonProperty("user_sr_flair_enabled")]
        public bool UserSrFlairEnabled { get; set; }

        [JsonProperty("user_sr_theme_enabled")]
        public bool UserSrThemeEnabled { get; set; }

        [JsonProperty("videostream_links_count")]
        public int VideostreamLinksCount { get; set; }

        [JsonProperty("whitelist_status")]
        public string WhitelistStatus { get; set; }

        [JsonProperty("wiki_enabled")]
        public bool? WikiEnabled { get; set; }

        [JsonProperty("wls")]
        public int Wls { get; set; }
    }
}
