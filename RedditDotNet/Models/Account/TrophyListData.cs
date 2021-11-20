using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Account
{
    public class TrophyListData
	{
		[JsonProperty("trophies")]
		public List<Award> Trophies { get; set; }
	}
}
