using Newtonsoft.Json;
using RedditDotNet.Models.Account;
using System.Collections.Generic;

namespace RedditDotNet.Models.Account
{
	public class UserList
	{
		[JsonProperty("children")]
		public List<User> Children { get; set; }
	}
}
