using Newtonsoft.Json;

namespace RedditDotNet.Models
{
    /// <summary>
    /// Base object for Reddit
    /// </summary>
    /// <typeparam name="T">Type of the data in the Thing</typeparam>
    public class Thing<T> where T : new()
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
