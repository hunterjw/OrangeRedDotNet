using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditDotNet.Models.Comments;
using System;

namespace RedditDotNet.Json
{
    /// <summary>
    /// Json converter for CommentBase
    /// </summary>
    public class CommentBaseConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(CommentBase).IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(CommentBase))
            {
                var jobj = JToken.Load(reader) as JObject;
                if (jobj.ContainsKey("children"))
                {
                    return jobj.ToObject<More>();
                }
                else
                {
                    return jobj.ToObject<Comment>();
                }
            }
            else
            {
                serializer.ContractResolver.ResolveContract(objectType).Converter = null;
                return serializer.Deserialize(reader, objectType);
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject.FromObject(value).WriteTo(writer);
        }
    }
}
