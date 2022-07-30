using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Multis
{
    /// <summary>
    /// MultiReddit data
    /// </summary>
    public class MultiRedditData
    {
        [JsonProperty("can_edit")]
        public bool CanEdit { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("num_subscribers")]
        public int NumSubscribers { get; set; }

        [JsonProperty("copied_from")]
        public string CopiedFrom { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("subreddits")]
        public List<MultiSubreddit> Subreddits { get; set; }

        [JsonProperty("created_utc")]
        public double CreatedUtc { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("created")]
        public double Created { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("is_subscriber")]
        public bool IsSubscriber { get; set; }

        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }

        [JsonProperty("description_md")]
        public string DescriptionMd { get; set; }

        [JsonProperty("is_favorited")]
        public bool IsFavorited { get; set; }
    }
}
