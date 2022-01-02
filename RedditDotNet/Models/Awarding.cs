using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditDotNet.Models
{
    /// <summary>
    /// Award given to Links and Comments
    /// </summary>
    public class Awarding
    {
        [JsonProperty("giver_coin_reward")]
        public int? GiverCoinReward { get; set; }

        // TODO
        //[JsonProperty("subreddit_id")]
        //public object SubredditId { get; set; }

        [JsonProperty("is_new")]
        public bool IsNew { get; set; }

        [JsonProperty("days_of_drip_extension")]
        public int DaysOfDripExtension { get; set; }

        [JsonProperty("coin_price")]
        public int CoinPrice { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("penny_donate")]
        public int? PennyDonate { get; set; }

        [JsonProperty("coin_reward")]
        public int CoinReward { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("days_of_premium")]
        public int DaysOfPremium { get; set; }

        [JsonProperty("icon_height")]
        public int IconHeight { get; set; }

        // TODO
        //[JsonProperty("tiers_by_required_awardings")]
        //public object TiersByRequiredAwardings { get; set; }

        [JsonProperty("resized_icons")]
        public List<ImageDetail> ResizedIcons { get; set; }

        [JsonProperty("icon_width")]
        public int IconWidth { get; set; }

        [JsonProperty("static_icon_width")]
        public int StaticIconWidth { get; set; }

        // TODO
        //[JsonProperty("start_date")]
        //public object StartDate { get; set; }

        [JsonProperty("is_enabled")]
        public bool IsEnabled { get; set; }

        // TODO
        //[JsonProperty("awardings_required_to_grant_benefits")]
        //public object AwardingsRequiredToGrantBenefits { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        // TODO
        //[JsonProperty("end_date")]
        //public object EndDate { get; set; }

        [JsonProperty("subreddit_coin_reward")]
        public int SubredditCoinReward { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("static_icon_height")]
        public int StaticIconHeight { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resized_static_icons")]
        public List<ImageDetail> ResizedStaticIcons { get; set; }

        [JsonProperty("icon_format")]
        public string IconFormat { get; set; }

        [JsonProperty("award_sub_type")]
        public string AwardSubType { get; set; }

        [JsonProperty("penny_price")]
        public int? PennyPrice { get; set; }

        [JsonProperty("award_type")]
        public string AwardType { get; set; }

        [JsonProperty("static_icon_url")]
        public string StaticIconUrl { get; set; }
    }

}
