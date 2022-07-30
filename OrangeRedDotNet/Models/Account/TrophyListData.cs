using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Account
{
    public class TrophyListData
	{
		[JsonProperty("trophies")]
		public List<Award> Trophies { get; set; }
	}
}
