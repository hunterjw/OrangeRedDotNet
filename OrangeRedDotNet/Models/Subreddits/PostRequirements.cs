using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Subreddits
{
    public class PostRequirements
    {
        [JsonProperty("body_blacklisted_strings")]
        public List<string> BodyBlacklistedStrings { get; set; }

        // TODO
        //[JsonProperty("body_regexes")]
        //public List<object> BodyRegexes { get; set; }

        [JsonProperty("body_required_strings")]
        public List<string> BodyRequiredStrings { get; set; }

        [JsonProperty("body_restriction_policy")]
        public string BodyRestrictionPolicy { get; set; }

        [JsonProperty("body_text_max_length")]
        public int? BodyTextMaxLength { get; set; }

        [JsonProperty("body_text_min_length")]
        public int? BodyTextMinLength { get; set; }

        [JsonProperty("domain_blacklist")]
        public List<string> DomainBlacklist { get; set; }

        [JsonProperty("domain_whitelist")]
        public List<string> DomainWhitelist { get; set; }

        [JsonProperty("gallery_captions_requirement")]
        public string GalleryCaptionsRequirement { get; set; }

        [JsonProperty("gallery_max_items")]
        public int? GalleryMaxItems { get; set; }

        [JsonProperty("gallery_min_items")]
        public int? GalleryMinItems { get; set; }

        [JsonProperty("gallery_urls_requirement")]
        public string GalleryUrlsRequirement { get; set; }

        [JsonProperty("guidelines_display_policy")]
        public string GuidelinesDisplayPolicy { get; set; }

        [JsonProperty("guidelines_text")]
        public string GuidelinesText { get; set; }

        [JsonProperty("is_flair_required")]
        public bool? IsFlairRequired { get; set; }

        // TODO
        //[JsonProperty("link_repost_age")]
        //public object LinkRepostAge { get; set; }

        [JsonProperty("link_restriction_policy")]
        public string LinkRestrictionPolicy { get; set; }

        [JsonProperty("title_blacklisted_strings")]
        public List<string> TitleBlacklistedStrings { get; set; }

        // TODO
        //[JsonProperty("title_regexes")]
        //public List<object> TitleRegexes { get; set; }

        [JsonProperty("title_required_strings")]
        public List<string> TitleRequiredStrings { get; set; }

        [JsonProperty("title_text_max_length")]
        public int? TitleTextMaxLength { get; set; }

        [JsonProperty("title_text_min_length")]
        public int? TitleTextMinLength { get; set; }
    }


}
