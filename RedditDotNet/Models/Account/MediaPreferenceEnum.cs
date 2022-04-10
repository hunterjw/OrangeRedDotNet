using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace RedditDotNet.Models.Account
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaPreference
    {
        [EnumMember(Value = "on")] [Description("On")] On,
        [EnumMember(Value = "off")] [Description("Off")] Off,
        [EnumMember(Value = "subreddit")][Description("Subreddit")] Subreddit
    }
}
