using Newtonsoft.Json;
using RedditDotNet.Models.Account;
using System.Collections.Generic;

namespace RedditDotNet.Models.DTO
{
	public class TrophyListData
	{
		[JsonProperty("trophies")]
		public List<Thing<Award>> Trophies { get; set; }
	}
}
