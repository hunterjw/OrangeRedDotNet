using Newtonsoft.Json;
using System;

namespace RedditDotNet.Authentication
{
	public class TokenResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty("scope")]
		public string Scope { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }

		public DateTime Retrieved { get; set; } = DateTime.MinValue;

		public DateTime Expires { get; set; } = DateTime.MinValue;
	}
}
