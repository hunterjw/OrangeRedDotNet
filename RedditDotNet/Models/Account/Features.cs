using Newtonsoft.Json;

namespace RedditDotNet.Models.Account
{
	public class Features
    {
        [JsonProperty("mod_service_mute_writes")]
        public bool ModServiceMuteWrites { get; set; }

        [JsonProperty("promoted_trend_blanks")]
        public bool PromotedTrendBlanks { get; set; }

        [JsonProperty("show_amp_link")]
        public bool ShowAmpLink { get; set; }

        [JsonProperty("chat")]
        public bool Chat { get; set; }

        [JsonProperty("is_email_permission_required")]
        public bool IsEmailPermissionRequired { get; set; }

        [JsonProperty("mod_awards")]
        public bool ModAwards { get; set; }

        [JsonProperty("mweb_xpromo_revamp_v3")]
        public Promo MWebXPromoRevampV3 { get; set; }

        [JsonProperty("mweb_xpromo_revamp_v2")]
        public Promo MWebXPromoRevampV2 { get; set; }

        [JsonProperty("awards_on_streams")]
        public bool AwardsOnStreams { get; set; }

        [JsonProperty("webhook_config")]
        public bool WebHookConfig { get; set; }

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_ios")]
        public bool MWebXPromoModalListingClickDailyDismissibleIOS { get; set; }

        [JsonProperty("live_orangereds")]
        public bool LiveOrangeReds { get; set; }

        [JsonProperty("cookie_consent_banner")]
        public bool CookieConsentBanner { get; set; }

        [JsonProperty("modlog_copyright_removal")]
        public bool ModlogCopyrightRemoval { get; set; }

        [JsonProperty("do_not_track")]
        public bool DoNotTrack { get; set; }

        [JsonProperty("mod_service_mute_reads")]
        public bool ModServiceMuteReads { get; set; }

        [JsonProperty("chat_user_settings")]
        public bool ChatUserSettings { get; set; }

        [JsonProperty("use_pref_account_deployment")]
        public bool UsePrefAccountDeployment { get; set; }

        [JsonProperty("mweb_xpromo_interstitial_comments_ios")]
        public bool MWebXPromoInterstitialCommentsIOS { get; set; }

        [JsonProperty("chat_subreddit")]
        public bool ChatSubreddit { get; set; }

        [JsonProperty("noreferrer_to_noopener")]
        public bool NoReferrerToNoOpener { get; set; }

        [JsonProperty("premium_subscriptions_table")]
        public bool PremiumSubscriptionsTable { get; set; }

        [JsonProperty("mweb_xpromo_interstitial_comments_android")]
        public bool MWebXPromoInterstitialCommentsAndroid { get; set; }

        [JsonProperty("mweb_sharing_web_share_api")]
        public Promo MWebSharingWebShareApi { get; set; }

        [JsonProperty("chat_group_rollout")]
        public bool ChatGroupRollout { get; set; }

        [JsonProperty("resized_styles_images")]
        public bool ResizedStylesImages { get; set; }

        [JsonProperty("spez_modal")]
        public bool SpezModal { get; set; }

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_android")]
        public bool MWebXPromoModalListingClickDailyDismissibleAndroid { get; set; }

        [JsonProperty("expensive_coins_package")]
        public bool ExpensiveCoinsPackage { get; set; }
    }
}
