using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models
{
    public class RedditApiError
    {
        [JsonProperty("fields")]
        public List<string> Fields { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
