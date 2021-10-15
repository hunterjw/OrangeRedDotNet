using Newtonsoft.Json;

namespace RedditDotNet.Models
{
	public class Thing<T> where T : new()
	{
		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }
	}
}
