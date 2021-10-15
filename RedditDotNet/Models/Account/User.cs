﻿using Newtonsoft.Json;

namespace RedditDotNet.Models.Account
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
	}
}
