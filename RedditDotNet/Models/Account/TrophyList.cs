using Newtonsoft.Json;
using RedditDotNet.Models.Account;
using System.Collections.Generic;

namespace RedditDotNet.Models.Account
{
	public class TrophyList
	{
		[JsonProperty("trophies")]
		public List<Thing<Award>> Trophies { get; set; }
	}
}
