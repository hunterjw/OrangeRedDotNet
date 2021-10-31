using Newtonsoft.Json;
using System;

namespace RedditDotNet.Models.Account
{
	public class Award
	{
		[JsonProperty("icon_70")]
		public string Icon70 { get; set; }

		[JsonProperty("granted_at")]
		public long? GrantedAt { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("icon_40")]
		public string Icon40 { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("award_id")]
		public string AwardId { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonIgnore]
		public DateTime GrantedAtDateTime
		{
			get
			{
				if (GrantedAt == default)
				{
					return DateTime.MinValue;
				}
				return DateTimeOffset.FromUnixTimeSeconds(GrantedAt.Value)
					.ToLocalTime().DateTime;
			}
		}
	}
}
