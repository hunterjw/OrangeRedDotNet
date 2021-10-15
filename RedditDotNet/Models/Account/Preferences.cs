using Newtonsoft.Json;

namespace RedditDotNet.Models.Account
{
	public class Preferences
	{
		[JsonProperty("beta")]
		public bool Beta { get; set; }

		[JsonProperty("default_theme_sr")]
		public string DefaultThemeSubreddit { get; set; }

		[JsonProperty("threaded_messages")]
		public bool ThreadedMessages { get; set; }

		[JsonProperty("email_comment_reply")]
		public bool EmailCommentReply { get; set; }

		[JsonProperty("private_feeds")]
		public bool PrivateFeeds { get; set; }

		[JsonProperty("activity_relevant_ads")]
		public bool ActivityRelevantAds { get; set; }

		[JsonProperty("email_messages")]
		public bool EmailMessages { get; set; }

		[JsonProperty("profile_opt_out")]
		public bool ProfileOptOut { get; set; }

		[JsonProperty("video_autoplay")]
		public bool VideoAutoplay { get; set; }

		[JsonProperty("accept_pms")]
		public string AcceptPms { get; set; }

		[JsonProperty("geopopular")]
		public string Geopopular { get; set; }

		[JsonProperty("show_link_flair")]
		public bool ShowLinkFlair { get; set; }

		[JsonProperty("show_trending")]
		public bool ShowTrending { get; set; }

		[JsonProperty("send_welcome_messages")]
		public bool SendWelcomeMessages { get; set; }

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }

		[JsonProperty("design_beta")]
		public bool DesignBeta { get; set; }

		[JsonProperty("monitor_mentions")]
		public bool MonitorMentions { get; set; }

		[JsonProperty("hide_downs")]
		public bool HideDowns { get; set; }

		[JsonProperty("clickgadget")]
		public bool Clickgadget { get; set; }

		[JsonProperty("lang")]
		public string Lang { get; set; }

		[JsonProperty("ignore_suggested_sort")]
		public bool IgnoreSuggestedSort { get; set; }

		[JsonProperty("show_presence")]
		public bool ShowPresence { get; set; }

		[JsonProperty("email_upvote_comment")]
		public bool EmailUpvoteComment { get; set; }

		[JsonProperty("email_digests")]
		public bool EmailDigests { get; set; }

		[JsonProperty("layout")]
		public int Layout { get; set; }

		[JsonProperty("num_comments")]
		public int NumComments { get; set; }

		[JsonProperty("feed_recommendations_enabled")]
		public bool FeedRecommendationsEnabled { get; set; }

		[JsonProperty("label_nsfw")]
		public bool LabelNsfw { get; set; }

		[JsonProperty("research")]
		public bool Research { get; set; }

		[JsonProperty("use_global_defaults")]
		public bool UseGlobalDefaults { get; set; }

		[JsonProperty("show_snoovatar")]
		public bool ShowSnoovatar { get; set; }

		[JsonProperty("over_18")]
		public bool Over18 { get; set; }

		[JsonProperty("legacy_search")]
		public bool LegacySearch { get; set; }

		[JsonProperty("live_orangereds")]
		public bool LiveOrangereds { get; set; }

		[JsonProperty("highlight_controversial")]
		public bool HighlightControversial { get; set; }

		[JsonProperty("no_profanity")]
		public bool NoProfanity { get; set; }

		[JsonProperty("domain_details")]
		public bool DomainDetails { get; set; }

		[JsonProperty("collapse_left_bar")]
		public bool CollapseLeftBar { get; set; }

		[JsonProperty("email_community_discovery")]
		public bool EmailCommunityDiscovery { get; set; }

		[JsonProperty("bad_comment_autocollapse")]
		public string BadCommentAutocollapse { get; set; }

		[JsonProperty("hide_ups")]
		public bool HideUps { get; set; }

		[JsonProperty("third_party_data_personalized_ads")]
		public bool ThirdPartyDataPersonalizedAds { get; set; }

		[JsonProperty("email_chat_request")]
		public bool EmailChatRequest { get; set; }

		[JsonProperty("allow_clicktracking")]
		public bool AllowClicktracking { get; set; }

		[JsonProperty("hide_from_robots")]
		public bool HideFromRobots { get; set; }

		[JsonProperty("show_twitter")]
		public bool ShowTwitter { get; set; }

		[JsonProperty("compress")]
		public bool Compress { get; set; }

		[JsonProperty("store_visits")]
		public bool StoreVisits { get; set; }

		[JsonProperty("threaded_modmail")]
		public bool ThreadedModmail { get; set; }

		[JsonProperty("email_upvote_post")]
		public bool EmailUpvotePost { get; set; }

		[JsonProperty("min_link_score")]
		public int MinLinkScore { get; set; }

		[JsonProperty("media_preview")]
		public string MediaPreview { get; set; }

		[JsonProperty("email_user_new_follower")]
		public bool EmailUserNewFollower { get; set; }

		[JsonProperty("nightmode")]
		public bool Nightmode { get; set; }

		[JsonProperty("enable_default_themes")]
		public bool EnableDefaultThemes { get; set; }

		[JsonProperty("third_party_site_data_personalized_content")]
		public bool ThirdPartySiteDataPersonalizedContent { get; set; }

		[JsonProperty("third_party_site_data_personalized_ads")]
		public bool ThirdPartySiteDataPersonalizedAds { get; set; }

		[JsonProperty("survey_last_seen_time")]
		public object SurveyLastSeenTime { get; set; }

		[JsonProperty("show_stylesheets")]
		public bool ShowStylesheets { get; set; }

		[JsonProperty("enable_followers")]
		public bool EnableFollowers { get; set; }

		[JsonProperty("min_comment_score")]
		public int MinCommentScore { get; set; }

		[JsonProperty("public_votes")]
		public bool PublicVotes { get; set; }

		[JsonProperty("email_post_reply")]
		public bool EmailPostReply { get; set; }

		[JsonProperty("collapse_read_messages")]
		public bool CollapseReadMessages { get; set; }

		[JsonProperty("show_flair")]
		public bool ShowFlair { get; set; }

		[JsonProperty("mark_messages_read")]
		public bool MarkMessagesRead { get; set; }

		[JsonProperty("search_include_over_18")]
		public bool SearchIncludeOver18 { get; set; }

		[JsonProperty("hide_ads")]
		public bool HideAds { get; set; }

		[JsonProperty("third_party_personalized_ads")]
		public bool ThirdPartyPersonalizedAds { get; set; }

		[JsonProperty("email_username_mention")]
		public bool EmailUsernameMention { get; set; }

		[JsonProperty("top_karma_subreddits")]
		public bool TopKarmaSubreddits { get; set; }

		[JsonProperty("newwindow")]
		public bool Newwindow { get; set; }

		[JsonProperty("numsites")]
		public int Numsites { get; set; }

		[JsonProperty("email_private_message")]
		public bool EmailPrivateMessage { get; set; }

		[JsonProperty("send_crosspost_messages")]
		public bool SendCrosspostMessages { get; set; }

		[JsonProperty("media")]
		public string Media { get; set; }

		[JsonProperty("public_server_seconds")]
		public bool PublicServerSeconds { get; set; }

		[JsonProperty("show_gold_expiration")]
		public bool ShowGoldExpiration { get; set; }

		[JsonProperty("highlight_new_comments")]
		public bool HighlightNewComments { get; set; }

		[JsonProperty("email_unsubscribe_all")]
		public bool EmailUnsubscribeAll { get; set; }

		[JsonProperty("default_comment_sort")]
		public string DefaultCommentSort { get; set; }

		[JsonProperty("show_location_based_recommendations")]
		public bool ShowLocationBasedRecommendations { get; set; }
	}
}
