using Newtonsoft.Json;
using RedditDotNet.Models.Account;
using System.Collections.Generic;

namespace RedditDotNet.Models.DTO
{
	public class UserListData
	{
		[JsonProperty("children")]
		public List<User> Children { get; set; }
	}
}
