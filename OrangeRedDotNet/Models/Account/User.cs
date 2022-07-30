using Newtonsoft.Json;
using System;

namespace OrangeRedDotNet.Models.Account
{
	public class User
	{
		[JsonProperty("date")]
		public decimal Date { get; set; }

		[JsonProperty("rel_id")]
		public string RelId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonIgnore]
		public DateTime DateDateTime
		{
			get
			{
				return DateTimeOffset.FromUnixTimeSeconds((long)Date)
					.ToLocalTime().DateTime;
			}
		}
	}
}
