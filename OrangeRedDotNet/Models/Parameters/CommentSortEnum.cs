using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace OrangeRedDotNet.Models.Parameters
{
    /// <summary>
    /// Comment sort enum
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentSort
    {
        [EnumMember(Value = "confidence")] [Description("confidence")] Confidence,
        [EnumMember(Value = "top")] [Description("top")] Top,
        [EnumMember(Value = "new")] [Description("new")] New,
        [EnumMember(Value = "controversial")] [Description("controversial")] Controversial,
        [EnumMember(Value = "old")] [Description("old")] Old,
        [EnumMember(Value = "random")] [Description("random")] Random,
        [EnumMember(Value = "qa")] [Description("qa")] QA,
        [EnumMember(Value = "live")] [Description("live")] Live
    }
}
