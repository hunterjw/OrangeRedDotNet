using Newtonsoft.Json;
using OrangeRedDotNet.Models.Listings;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Comments
{
    /// <summary>
    /// A comment on a Link on Reddit
    /// </summary>
    public class CommentData : CommentBaseData
    {
        [JsonProperty("replies")]
        public Listing<CommentBase> Replies { get; set; }

        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonProperty("approved_at_utc")]
        public string ApprovedAtUtc { get; set; } // TODO Convert to a DateTime

        [JsonProperty("author_is_blocked")]
        public bool AuthorIsBlocked { get; set; }

        // TODO
        //[JsonProperty("comment_type")]
        //public object CommentType { get; set; }

        // TODO
        //[JsonProperty("awarders")]
        //public List<object> Awarders { get; set; }

        [JsonProperty("mod_reason_by")]
        public string ModReasonBy { get; set; }

        [JsonProperty("banned_by")]
        public string BannedBy { get; set; }

        [JsonProperty("author_flair_type")]
        public string AuthorFlairType { get; set; }

        [JsonProperty("total_awards_received")]
        public int TotalAwardsReceived { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("author_flair_template_id")]
        public string AuthorFlairTemplateId { get; set; }

        [JsonProperty("likes")]
        public bool? Likes { get; set; }

        // TODO
        //[JsonProperty("user_reports")]
        //public List<object> UserReports { get; set; }

        [JsonProperty("saved")]
        public bool Saved { get; set; }

        [JsonProperty("banned_at_utc")]
        public string BannedAtUtc { get; set; } // TODO Convert to DateTime

        [JsonProperty("mod_reason_title")]
        public string ModReasonTitle { get; set; }

        [JsonProperty("gilded")]
        public int Gilded { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        // TODO
        //[JsonProperty("collapsed_reason_code")]
        //public object CollapsedReasonCode { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("can_mod_post")]
        public bool CanModPost { get; set; }

        [JsonProperty("created_utc")]
        public double CreatedUtc { get; set; }

        [JsonProperty("send_replies")]
        public bool SendReplies { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("author_fullname")]
        public string AuthorFullname { get; set; }

        [JsonProperty("approved_by")]
        public string ApprovedBy { get; set; }

        [JsonProperty("mod_note")]
        public string ModNote { get; set; }

        [JsonProperty("all_awardings")]
        public List<Awarding> AllAwardings { get; set; }

        [JsonProperty("collapsed")]
        public bool Collapsed { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("edited")]
        public bool Edited { get; set; }

        // TODO
        //[JsonProperty("top_awarded_type")]
        //public object TopAwardedType { get; set; }

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCssClass { get; set; }

        [JsonProperty("is_submitter")]
        public bool IsSubmitter { get; set; }

        [JsonProperty("downs")]
        public int Downs { get; set; }

        [JsonProperty("author_flair_richtext")]
        public List<FlairRichtext> AuthorFlairRichtext { get; set; }

        [JsonProperty("author_patreon_flair")]
        public bool AuthorPatreonFlair { get; set; }

        [JsonProperty("body_html")]
        public string BodyHtml { get; set; }

        [JsonProperty("removal_reason")]
        public string RemovalReason { get; set; }

        [JsonProperty("collapsed_reason")]
        public string CollapsedReason { get; set; }

        [JsonProperty("distinguished")]
        public string Distinguished { get; set; }

        // TODO
        //[JsonProperty("associated_award")]
        //public object AssociatedAward { get; set; }

        [JsonProperty("stickied")]
        public bool Stickied { get; set; }

        [JsonProperty("author_premium")]
        public bool AuthorPremium { get; set; }

        [JsonProperty("can_gild")]
        public bool CanGild { get; set; }

        // TODO
        //[JsonProperty("gildings")]
        //public object Gildings { get; set; }

        // TODO
        //[JsonProperty("unrepliable_reason")]
        //public object UnrepliableReason { get; set; }

        [JsonProperty("author_flair_text_color")]
        public string AuthorFlairTextColor { get; set; }

        [JsonProperty("score_hidden")]
        public bool ScoreHidden { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("report_reasons")]
        public List<string> ReportReasons { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("author_flair_text")]
        public string AuthorFlairText { get; set; }

        // TODO
        //[JsonProperty("treatment_tags")]
        //public List<object> TreatmentTags { get; set; }

        [JsonProperty("link_id")]
        public string LinkId { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("controversiality")]
        public int Controversiality { get; set; }

        [JsonProperty("author_flair_background_color")]
        public string AuthorFlairBackgroundColor { get; set; }

        // TODO
        //[JsonProperty("collapsed_because_crowd_control")]
        //public object CollapsedBecauseCrowdControl { get; set; }

        [JsonProperty("mod_reports")]
        public List<ListingData<string>> ModReports { get; set; }

        [JsonProperty("num_reports")]
        public int? NumReports { get; set; }

        [JsonProperty("ups")]
        public int Ups { get; set; }

        [JsonProperty("link_author")]
        public string LinkAuthor { get; set; }

        [JsonProperty("link_permalink")]
        public string LinkPermalink { get; set; }

        [JsonProperty("link_title")]
        public string LinkTitle { get; set; }

        [JsonProperty("link_url")]
        public string LinkUrl { get; set; }

        [JsonProperty("num_comments")]
        public int NumComments { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("quarantine")]
        public bool Quarantine { get; set; }
    }
}
