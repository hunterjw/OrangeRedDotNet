using Newtonsoft.Json;

namespace RedditDotNet.Models.Account
{
	public class Promo
	{
		[JsonProperty("owner")]
		public string Owner { get; set; }

		[JsonProperty("variant")]
		public string Variant { get; set; }

		[JsonProperty("experiment_id")]
		public int ExperimentId { get; set; }
	}
}
