using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Account
{
    public class UserListData
	{
		[JsonProperty("children")]
		public List<User> Children { get; set; }
	}
}
